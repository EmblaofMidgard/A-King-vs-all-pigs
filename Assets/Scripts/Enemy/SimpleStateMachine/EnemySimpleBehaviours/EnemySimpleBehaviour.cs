using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyLight))]
public abstract class EnemySimpleBehaviour : MonoBehaviour
{
    public Animator animator;
    public EnemyLight enemyLight;
    public NavMeshAgent agent;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyLight = GetComponent<EnemyLight>();
        agent = GetComponent<NavMeshAgent>();
    }

    public abstract void EnemyIdle();

    public abstract void EnemyChase();

    public abstract void EnemyAttack();
}