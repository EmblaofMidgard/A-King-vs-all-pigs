using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyBehaviour : EnemySimpleBehaviour
{
    public GameObject path;
    public GameObject enemyMeleeHitbox;

    public override void EnemyAttack()
    {
        
    }

    public override void EnemyChase()
    {
        
    }

    public override void EnemyIdle()
    {
        if (!enemyLight.lightOn)
            enemyLight.AccendiTorcia();
    }

    private void Start()
    {
        enemyMeleeHitbox.SetActive(false);
    }

    public void SetHitBox(bool v)
    {
        enemyMeleeHitbox.SetActive(v);
    }

}
