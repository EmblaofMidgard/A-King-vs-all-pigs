using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyLight))]
public abstract class EnemyBehaviour : MonoBehaviour
{
    public Animator animator;
    public EnemyLight enemyLight;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyLight = GetComponent<EnemyLight>();
    }

    public abstract void EnemyStartAttack();

    public abstract void EnemyDeath();

    public abstract void EnemyEndAttack();

    public abstract void EnemyCloseAttack();

    public abstract void EnemyFarAttack();

    public abstract void EnemyAlert();

    public abstract void EnemyNotAlert();

}