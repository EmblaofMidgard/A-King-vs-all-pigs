using System;
using UnityEngine;

public partial class Enemy
{
    class SimpleAttack : IState<Enemy>
    {
        private float activeTime = 2f;
        private float unactiveTime = 3f;
        private float elapsed;
        private bool isActive;

        public SimpleAttack(Enemy owner)
        {
            this.owner = owner;
        }

        public override void Enter()
        {
            Debug.Log($"{owner.gameObject.name} is {nameof(SimpleAttack)} at {Time.time}");
            owner.animator.SetBool($"{nameof(SimpleAttack)}", true);
            isActive = false;
            elapsed = 0f;
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
            }
            else
            {
                if (elapsed > unactiveTime)
                {
                    owner.SetMeleeHitboxState(true);
                    elapsed = 0f;
                    isActive = true;
                }
            }
            elapsed += Time.deltaTime;
        }

        public override void Exit()
        {
            owner.animator.SetBool($"{nameof(SimpleAttack)}", false);
            owner.SetMeleeHitboxState(false);
        }

    }

}
