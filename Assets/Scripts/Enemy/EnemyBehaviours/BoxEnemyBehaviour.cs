using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxEnemyBehaviour : EnemyBehaviour
{
    public float timeLightOn = 1f;
    public float timeLightOff = 1f;
    float elapsed = 0;
    bool lightOn => enemyLight.lightOn;

    public GameObject boxPrefab;
    public GameObject bombPrefab;

    public override void EnemyAlert()
    {
        if (lightOn)
        {
            if (elapsed < timeLightOn)
                elapsed += Time.deltaTime;
            else
            {
                enemyLight.SpegniTorcia();
                elapsed = 0;
            }
        }
        else
        {
            if (elapsed < timeLightOff)
                elapsed += Time.deltaTime;
            else
            {
                enemyLight.AccendiTorcia();
                elapsed = 0;
            }
        }
    }

    public override void EnemyCloseAttack()
    {
        throw new System.NotImplementedException();
    }

    public override void EnemyDeath()
    {
        throw new System.NotImplementedException();
    }

    public override void EnemyEndAttack()
    {
        throw new System.NotImplementedException();
    }

    public override void EnemyFarAttack()
    {
        throw new System.NotImplementedException();
    }

    public override void EnemyNotAlert()
    {
        
    }

    public override void EnemyStartAttack()
    {
        ThrownAttack(boxPrefab);
    }

    void ThrownAttack(GameObject projectile)
    {
        GameObject thrown = Instantiate(projectile);

    }

}
