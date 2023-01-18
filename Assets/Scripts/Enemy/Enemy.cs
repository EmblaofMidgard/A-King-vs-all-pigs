using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy : MonoBehaviour
{
    public StateMachine<Enemy> machine;

    public Transform target;

    public float speed = 10;
    public float enemyDetectionRange = 1f; //forse non serve
    public float enemyMeleeRange = 1f;

    public bool playerPresence { get; private set; }
    public bool playerSpotted => player.isInLight && playerPresence;
    public bool playerIsInMeleeRange;

    public EnemySimpleBehaviour behaviour;
    public EnemyLight enemyLight;
    public EnemyEye enemyEye;
    public Animator animator;
    CharacterControllerPlatformer2D player;
    Vector3 startingPoint;
    public float reachPoint = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        machine = new StateMachine<Enemy>(new Idle(this));
        startingPoint = transform.position;
        animator = GetComponent<Animator>();
        enemyLight = GetComponent<EnemyLight>();
        behaviour = GetComponent<EnemySimpleBehaviour>();
        enemyEye = GetComponentInChildren<EnemyEye>();
        player = target.gameObject.GetComponent<CharacterControllerPlatformer2D>();
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
            if (Vector2.Distance(target.position, transform.position) < enemyMeleeRange)
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

    


    //===OLD======================================
    //public float playerFarMaxDistance;
    //public float playerCloseMaxDistance;

    //float playerDistance;

    //public bool PlayerIsClose()
    //{
    //    UpdatePlayerDistance();
    //    Debug.Log($"{ playerDistance} close");
    //    return playerDistance < playerCloseMaxDistance;
    //}

    //public bool PlayerIsFar()
    //{
    //    UpdatePlayerDistance();
    //    Debug.Log($"{ playerDistance} far");
    //    return playerDistance < playerFarMaxDistance;
    //}

    //public float GetPlayerDistance()
    //{
    //    UpdatePlayerDistance();
    //    float distance = playerDistance;
    //    return distance;
    //}

    //void UpdatePlayerDistance()
    //{
    //    playerDistance = Vector2.Distance(transform.position, target.position);
    //    Debug.Log($"{ playerDistance} calculate");
    //}

}


//Idle->Chase->Attack->Idle