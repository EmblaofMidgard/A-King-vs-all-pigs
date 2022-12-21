using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState{Grounded,Jumping,Falling }

[RequireComponent(typeof(Rigidbody2D),typeof(Animator))]
public class CharacterControllerPlatformer2D : MonoBehaviour
{
    public float maxSpeed;
    public float acceleration;
    public float jumpForce;
    public float jumpAbortForce;
    public float stoppingSpeed;

    public Interacter interactionPoint;

    Vector2 moveVector;
    private Rigidbody2D body;

    PlayerState state;
    bool inputJump;
    float horizontalInput;
    float horizontalMove;


    bool jumpAbort;


    bool isGrounded;
    [SerializeField] Transform groundCheck;
    public LayerMask groundMask;


    Animator animator;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        state = PlayerState.Grounded;
        animator = GetComponent<Animator>();
    }




    private void Update()
    {
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

    }



    private void FixedUpdate()
    {
        ResetMotion();
        CheckForGround();
        UpdateHorizontal();

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

        moveVector.x =  horizontalMove;

    }

    private void CheckForGround()
    {
        isGrounded= Physics2D.OverlapCircle(groundCheck.position, 0.3f,groundMask);
        
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

}
