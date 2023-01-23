using System;
using UnityEngine;

public partial class Enemy
{
    class SimpleAttack : IState<Enemy>
    {
        private float activeTime = 0.5f;
        private float unactiveTime = 0.75f;
        private float elapsed;
        private bool isActive;
        private Vector3 startPosition;

        public SimpleAttack(Enemy owner)
        {
            this.owner = owner;
        }

        public override void Enter()
        {
            Debug.Log($"{owner.gameObject.name} is {nameof(SimpleAttack)} at {Time.time}");
            owner.animator.SetTrigger($"{nameof(SimpleAttack)}");
            isActive = false;
            elapsed = 0f;
            startPosition = owner.enemyMeleeHitbox.transform.localPosition;
        }

        public override void Execute()
        {
            if (!owner.playerIsInMeleeRange)
                owner.machine.SetState(new Chase(owner));
            else if (!owner.playerSpotted)
                owner.machine.SetState(new Idle(owner));
            else
                Attack();
        }

        private void Attack()
        {
            if (isActive)
            {
                if(elapsed > activeTime)
                {
                    owner.SetMeleeHitboxState(false);
                    elapsed = 0f;
                    isActive = false;
                    
                }
                owner.enemyMeleeHitbox.transform.localPosition = new Vector3(owner.enemyMeleeHitbox.transform.localPosition.x, owner.enemyMeleeHitbox.transform.localPosition.y + 0.001f, owner.enemyMeleeHitbox.transform.localPosition.z);
            }
            else
            {
                if (elapsed > unactiveTime)
                {
                    owner.SetMeleeHitboxState(true);
                    elapsed = 0f;
                    isActive = true;
                    owner.enemyMeleeHitbox.transform.localPosition = startPosition;
                }
            }
            elapsed += Time.deltaTime;
        }

        public override void Exit()
        {
            owner.SetMeleeHitboxState(false);
            owner.enemyMeleeHitbox.transform.localPosition = startPosition;
        }

    }

}
