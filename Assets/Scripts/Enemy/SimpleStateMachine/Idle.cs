using UnityEngine;

public partial class Enemy
{
    class Idle : IState<Enemy>
    {
        public Idle(Enemy owner)
        {
            this.owner = owner;
        }

        public override void Enter()
        {
            Debug.Log($"{owner.gameObject.name} is {nameof(Idle)} at {Time.time}");
            owner.animator.SetBool($"{nameof(Idle)}", true);
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
                if (!owner.enemyLight.lightOn)
                    owner.enemyLight.AccendiTorcia();
            }
                
        }

        public override void Exit()
        {
            owner.animator.SetBool($"{nameof(Idle)}", false);
            owner.lastTarget = owner.actualTarget;
        }

    }

}
