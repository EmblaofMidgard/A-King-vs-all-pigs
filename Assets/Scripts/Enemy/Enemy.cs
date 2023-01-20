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

    public float enemyDetectionRange = 1f; //forse non serve
    public float enemyMeleeRange = 1f;

    public bool playerPresence { get; private set; }
    public bool playerSpotted => player.isInLight && playerPresence;
    private bool playerIsInMeleeRange;

    public EnemyLight enemyLight;
    public EnemyEye enemyEye;
    public Animator animator;
    public NavMeshAgent agent;
    public CharacterControllerPlatformer2D player;

    public Transform patrollingPoints;
    public GameObject enemyMeleeHitbox;
    public Transform actualTarget;
    private NavMeshPath path;
    private float elapsed;
    private int currentIndex;
    private bool directionRight;

    private Transform lastTarget;
    
    // Start is called before the first frame update
    void Start()
    {
        machine = new StateMachine<Enemy>(new Idle(this));
        animator = GetComponent<Animator>();
        enemyLight = GetComponent<EnemyLight>();
        enemyEye = GetComponentInChildren<EnemyEye>();
        player = playerTarget.gameObject.GetComponent<CharacterControllerPlatformer2D>();
        agent = GetComponent<NavMeshAgent>();
        enemyMeleeHitbox.SetActive(false);
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.updatePosition = false;
        currentIndex = 0;
        directionRight = true;
        path = new NavMeshPath();
        elapsed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        machine.StateUpdate();
        SetPlayerIsInRange();
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

    private void Move()
    {
        elapsed += Time.deltaTime;
        if (elapsed > 1.0f)
        {
            elapsed -= 1.0f;
            NavMesh.CalculatePath(transform.position, actualTarget.position, NavMesh.AllAreas, path);
        }
        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
        //owner.transform.Translate(path.corners[0] - owner.transform.position * owner.speed * Time.deltaTime);
        if (path.corners.Length > 1)
        {
            Vector3 targetPoint = path.corners[1] - transform.position;
            if (Mathf.Abs(path.corners[1].y - transform.position.y) > jumpDistance)
                targetPoint.y += jumpForce;
            transform.Translate(targetPoint * speed * Time.deltaTime);
        }
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
        enemyMeleeHitbox.gameObject.SetActive(v);
    }

}


//Idle->Chase->Attack->Idle