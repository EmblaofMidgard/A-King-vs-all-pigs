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
                owner.behaviour.EnemyIdle();
        }

        public override void Exit()
        {
            owner.animator.SetBool($"{nameof(Idle)}", false);
        }

    }

}
