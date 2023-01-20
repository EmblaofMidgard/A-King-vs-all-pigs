using UnityEngine;

public partial class Enemy
{
    class Chase : IState<Enemy>
    {
        public Chase(Enemy owner)
        {
            this.owner = owner;
        }

        public override void Enter()
        {
            Debug.Log($"{owner.gameObject.name} is {nameof(Chase)} at {Time.time}");
            owner.animator.SetBool($"{nameof(Chase)}", true);
            owner.actualTarget = owner.playerTarget;
        }

        public override void Execute()
        {
            if (owner.playerIsInMeleeRange)
                owner.machine.SetState(new SimpleAttack(owner));
            else if (!owner.playerSpotted)
                owner.machine.SetState(new Idle(owner));
            else
                owner.Move();
        }

        public override void Exit()
        {
            owner.animator.SetBool($"{nameof(Chase)}", false);
        }

    }

}
