using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy : MonoBehaviour
{
    public StateMachine<Enemy> machine;

    public Transform target;

    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        machine = new StateMachine<Enemy>(new Idle(this));
    }

    // Update is called once per frame
    void Update()
    {
        machine.StateUpdate();
        
    }
}
