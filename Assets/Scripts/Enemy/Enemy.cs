using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy : MonoBehaviour
{
    public StateMachine<Enemy> machine;

    public Transform target;

    public float speed = 10;
    public float playerFarMaxDistance;
    public float playerCloseMaxDistance;

    float playerDistance;

    bool playerPresence;
    bool playerSpotted => enemyLight.lightOn;

    public EnemyBehaviour behaviour;
    public EnemyLight enemyLight;
    public Animator animator;
    Vector3 startingPoint;
    public float reachPoint = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        machine = new StateMachine<Enemy>(new NotAlert(this));
        playerPresence = false;
        startingPoint = transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        machine.StateUpdate();
    }

    public float GetPlayerDistance()
    {
        UpdatePlayerDistance();
        float distance = playerDistance;
        return distance;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, playerCloseMaxDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerFarMaxDistance);
    }
    
    public bool PlayerIsClose()
    {
        UpdatePlayerDistance();
        Debug.Log($"{ playerDistance} close");
        return playerDistance < playerCloseMaxDistance;
    }

    public bool PlayerIsFar()
    {
        UpdatePlayerDistance();
        Debug.Log($"{ playerDistance} far");
        return playerDistance < playerFarMaxDistance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterControllerPlatformer2D>() == true)
        {
            playerPresence = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterControllerPlatformer2D>() == true)
            playerPresence = false;
    }

    void UpdatePlayerDistance()
    {
        playerDistance = Vector2.Distance(transform.position, target.position);
        Debug.Log($"{ playerDistance} calculate");
    }

}
