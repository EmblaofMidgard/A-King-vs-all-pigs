using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemyBehaviour : EnemySimpleBehaviour
{
    public Transform patrollingPoints;
    public GameObject enemyMeleeHitbox;
    public Enemy owner;

    public Transform actualTarget;
    private NavMeshPath path;
    private float elapsed = 0.0f;

    public float reachPoint = 1f;
    private int currentIndex = 0;
    private bool directionRight;
    Transform lastTarget;
    

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
        Path();
        Move();
    }

    private void Start()
    {
        enemyMeleeHitbox.SetActive(false);
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.updatePosition = false;
        owner = GetComponent<Enemy>();
        currentIndex = 0;
        directionRight = true;
        path = new NavMeshPath();
        elapsed = 0.0f;
    }

    public void SetHitBox(bool v)
    {
        enemyMeleeHitbox.SetActive(v);
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
        if (elapsed > 2.0f)
        {
            elapsed -= 2.0f;
            NavMesh.CalculatePath(transform.position, actualTarget.position, NavMesh.AllAreas, path);
        }
        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
        //owner.transform.Translate(path.corners[0] - owner.transform.position * owner.speed * Time.deltaTime);
        if (path.corners.Length > 1)
        {
            Vector3 targetPoint = path.corners[1] - owner.transform.position;
            owner.transform.Translate(targetPoint * owner.speed * Time.deltaTime);
        }
    }

    private bool CheckReachPoint()
    {
        if (owner.transform.position.x - actualTarget.position.x < reachPoint)
            return true;
        else
            return false;
    }
}
