using UnityEngine;

public partial class Enemy
{
    class SimpleAttack : IState<Enemy>
    {
        public SimpleAttack(Enemy owner)
        {
            this.owner = owner;
        }

        public override void Enter()
        {
            Debug.Log($"{owner.gameObject.name} is {nameof(SimpleAttack)} at {Time.time}");
            owner.animator.SetBool($"{nameof(SimpleAttack)}", true);
        }

        public override void Execute()
        {
            if (!owner.playerIsInMeleeRange)
                owner.machine.SetState(new Chase(owner));
            else if (!owner.playerSpotted)
                owner.machine.SetState(new Idle(owner));
            else
                owner.behaviour.EnemyAttack();
        }

        public override void Exit()
        {
            owner.animator.SetBool($"{nameof(SimpleAttack)}", false);
        }

    }

}
