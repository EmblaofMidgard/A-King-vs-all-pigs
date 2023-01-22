using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public partial class Enemy : MonoBehaviour
{
    public StateMachine<Enemy> machine;

    public Transform playerTarget;

    public float speed = 10;
    public float reachPoint = 0.5f;
    public float jumpForce = 5f;
    public float jumpDistance = 1f;
    public float overlapRange = 0.2f;
    public LayerMask wallMask;

    public float enemyDetectionRange = 1f; //forse non serve
    public float enemyMeleeRange = 1f;
    public float idleDuration = 1f;
    public float deadCorpseRemainTime = 5f;

    public bool playerPresence { get; private set; }
    public bool playerSpotted => player.isInLight && playerPresence;
    private bool playerIsInMeleeRange;

    public EnemyLight enemyLight;
    public EnemyEye enemyEye;
    public Animator animator;
    public NavMeshAgent agent;
    public CharacterControllerPlatformer2D player;
    public Transform wallCheckPoint;
    public Transform patrollingPoints;
    public GameObject enemyMeleeHitbox;
    public Transform actualTarget;
    private NavMeshPath path;
    private float elapsed;
    private int currentIndex;
    private bool directionRight;
    private bool nearToWall;
    private bool isDead;

    private Transform lastTarget;
    
    // Start is called before the first frame update
    void Start()
    {
        machine = new StateMachine<Enemy>(new Idle(this));
        animator = GetComponentInChildren<Animator>();
        enemyLight = GetComponent<EnemyLight>();
        enemyEye = GetComponentInChildren<EnemyEye>();
        player = playerTarget.gameObject.GetComponent<CharacterControllerPlatformer2D>();
        agent = GetComponent<NavMeshAgent>();
        enemyMeleeHitbox.GetComponent<Damager>().hitboxCollider.enabled = false;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.updatePosition = false;
        currentIndex = 0;
        directionRight = true;
        path = new NavMeshPath();
        elapsed = 0.0f;
        nearToWall = false;
        SetIsDead(false);
    }

    // Update is called once per frame
    void Update()
    {
        machine.StateUpdate();
        SetPlayerIsInRange();
        CheckTorchStatus();
    }

    private void CheckTorchStatus()
    {
        if (!enemyLight.lightOn && !playerSpotted)
            machine.SetState(new LightTorch(this));
    }

    private void SetPlayerIsInRange()
    {
        if (playerSpotted)
        {
            if (Vector2.Distance(playerTarget.position, transform.position) < enemyMeleeRange)
                playerIsInMeleeRange = true;
            else
                playerIsInMeleeRange = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyDetectionRange);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, enemyMeleeRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(wallCheckPoint.position, overlapRange);

    }
    
    internal void SetPlayerPresence(bool v)
    {
        playerPresence = v;
    }

    private void Path()
    {
        if (CheckReachPoint())
        {
            if (directionRight)
                currentIndex++;
            else
                currentIndex--;

            if (currentIndex == patrollingPoints.childCount - 1)
                this.directionRight = false;
            if (currentIndex == 0)
                this.directionRight = true;
            currentIndex %= patrollingPoints.childCount;
            actualTarget = patrollingPoints.GetChild(currentIndex);
        }
    }

    private Vector2 CalculateHorzontalMove(Vector2 targetPoint)
    {
        Vector2 newTarget = targetPoint;
        if (path.corners.Length > 1)
        {
             newTarget = new Vector2(path.corners[1].x - transform.position.x, 0);
        }
        return newTarget;
    }

    private void CalculatePathPoints()
    {
        elapsed += Time.deltaTime;
        if (elapsed > 1.0f)
        {
            elapsed -= 1.0f;
            NavMesh.CalculatePath(transform.position, actualTarget.position, NavMesh.AllAreas, path);
        }
        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
    }

    private Vector2 CalculateVerticalMove(Vector2 targetPoint)
    {
        Vector2 newTarget = targetPoint;
        if (nearToWall)
        {
            if (Mathf.Abs(path.corners[1].y - transform.position.y) > jumpDistance)
                newTarget.y += jumpForce;
        }
        return newTarget;
    }

    private void Move()
    {
        Vector2 targetPoint = Vector2.zero;
        CalculatePathPoints();
        CheckWallProximity();
        targetPoint = CalculateHorzontalMove(targetPoint);
        targetPoint = CalculateVerticalMove(targetPoint);
        transform.Translate(targetPoint * speed * Time.deltaTime);
        CheckDirection(actualTarget.transform.position, transform.position);
    }

    private void CheckDirection(Vector2 targetPoint, Vector3 position)
    {
        if(position.x - targetPoint.x < 0)
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
    }

    private void CheckWallProximity()
    {
        nearToWall = Physics2D.OverlapCircle(wallCheckPoint.position, overlapRange, wallMask);
    }

    private bool CheckReachPoint()
    {
        if (Mathf.Abs(transform.position.x - actualTarget.position.x) < reachPoint)
            return true;
        else
            return false;
    }

    public void SetMeleeHitboxState(bool v)
    {
        enemyMeleeHitbox.GetComponent<Damager>().hitboxCollider.enabled = v;
    }


    public void SetIsDead(bool v)
    {
        isDead = v;
    }

    public void Die()
    {
        machine.SetState(new Dead(this));
    }
    public void AnimatorHit()
    {
        animator.SetTrigger("Hitted");
    }

}


//Idle->Chase->Attack->Idle