using UnityEngine;

public partial class Enemy
{
    class Patrol : IState<Enemy>
    {
        public Patrol(Enemy owner)
        {
            this.owner = owner;
        }

        public override void Enter()
        {
            Debug.Log($"{owner.gameObject.name} is {nameof(Patrol)} at {Time.time}");
            owner.animator.SetBool($"{nameof(Patrol)}", true);
            if(owner.lastTarget != null)
                owner.actualTarget = owner.lastTarget;
        }

        public override void Execute()
        {
            if (owner.playerSpotted == true)
            {
                if (owner.playerIsInMeleeRange)
                    owner.machine.SetState(new SimpleAttack(owner));
                else
                    owner.machine.SetState(new Chase(owner));
            }
            else
            {
                owner.Path();
                owner.Move();
                //if (!owner.enemyLight.lightOn)
                //    owner.enemyLight.AccendiTorcia();
            }
                
        }

        public override void Exit()
        {
            owner.animator.SetBool($"{nameof(Patrol)}", false);
            owner.lastTarget = owner.actualTarget;
        }

    }

}
