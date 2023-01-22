using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState{Grounded,Jumping,Falling }

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterControllerPlatformer2D : MonoBehaviour
{
    public float maxSpeed;
    public float acceleration;
    public float jumpForce;
    public float jumpAbortForce;
    public float stoppingSpeed;
    public float overlapRange = 0.3f;

    public float attackActiveTime = 0.3f;
    private float elapsed;
    private bool hitboxIsActive;
    public Interacter interactionPoint;
    public GameObject attackHitbox;

    Vector2 moveVector;
    private Rigidbody2D body;
    private bool isDead;

    PlayerState state;
    bool inputJump;
    float horizontalInput;
    float horizontalMove;

    bool jumpAbort;
    public bool isInLight {get; private set;}

    bool isGrounded;
    [SerializeField] Transform groundCheck;
    public LayerMask groundMask;

    Animator animator;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        state = PlayerState.Grounded;
        animator = GetComponentInChildren<Animator>();
        SetisAliveTrue();
        attackHitbox.GetComponent<Damager>().hitboxCollider.enabled = false;
        elapsed = 0;
        hitboxIsActive = false;
    }

    private void Update()
    {
        if (isDead)
            return;
        
        if (isGrounded && Input.GetButtonDown("Jump"))
            inputJump = true;

        if (body.velocity.y > jumpAbortForce && Input.GetButtonUp("Jump"))
            jumpAbort = true;


        if (Input.GetButtonDown("Use"))
            interactionPoint.TryInteract();
        
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (transform.localScale.x * horizontalInput < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        if (Input.GetButtonDown("Attack"))
        {
            if(!hitboxIsActive)
                Attack();
        }

    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        SetHitboxOn();
    }

    private void CheckAttack()
    {
        if (hitboxIsActive)
        {
            if (elapsed > attackActiveTime)
            {
                SetHitboxOff();
                elapsed = 0f;
            }
            else
                elapsed += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (isDead)
            return;
        
        ResetMotion();
        CheckForGround();
        UpdateHorizontal();
        CheckAttack();

        switch (state)
        {


            case PlayerState.Grounded:

                CheckJump();
                break;

            case PlayerState.Jumping:
                CheckAbortJump();
                if (body.velocity.y < 0)
                    state = PlayerState.Falling;

                break;

            case PlayerState.Falling:
                if (isGrounded)
                    state = PlayerState.Grounded;
                break;
        }

        body.velocity = moveVector;

        animator.SetFloat("horizontalSpeed", Mathf.Abs( body.velocity.x));
        animator.SetFloat("verticalSpeed", body.velocity.y);
        animator.SetBool("grounded", isGrounded);
    }


    private void ResetMotion()
    {
        moveVector = body.velocity;
    }

    private void UpdateHorizontal()
    {
        horizontalMove +=horizontalInput * acceleration * Time.fixedDeltaTime;
        if (horizontalInput == 0)
            horizontalMove = Mathf.Lerp(horizontalMove, 0, stoppingSpeed);


        horizontalMove = Mathf.Clamp(horizontalMove, -maxSpeed, maxSpeed);

        moveVector.x = horizontalMove;
    }

    private void CheckForGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, overlapRange, groundMask);

    }

    private void CheckJump()
    {
        if (inputJump && isGrounded)
        {
            inputJump = false;
            moveVector = Vector2.up * jumpForce;

            animator.SetTrigger("jump");
            state = PlayerState.Jumping;
        }
    }

    private void CheckAbortJump()
    {
        if (jumpAbort && body.velocity.y > jumpAbortForce)
        {
            moveVector = Vector2.up * jumpAbortForce;
            jumpAbort = false;
        }
    }

    internal void SetIsInLight(bool v)
    {
        isInLight = v;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.position, overlapRange);
    }

    public void HitAnimationtrigger()
    {
        animator.SetTrigger("isHitted");
    }

    public void SetAnimatorisAlive(bool v)
    {
        animator.SetBool("isAlive", v);
    }

    public void SetisAliveTrue()
    {
        SetAnimatorisAlive(true);
        isDead=false;
    }

    public void SetisAliveFalse()
    {
        SetAnimatorisAlive(false);
        isDead = true;
    }

    public void SetHitboxOff()
    {
        attackHitbox.GetComponent<Damager>().hitboxCollider.enabled = false;
        hitboxIsActive = false;
    }

    public void SetHitboxOn()
    {
        attackHitbox.GetComponent<Damager>().hitboxCollider.enabled = true;
        hitboxIsActive = true;
    }
}
