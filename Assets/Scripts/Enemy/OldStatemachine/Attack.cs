using UnityEngine;

public partial class Enemy
{
    class Attack : IState<Enemy>
    {
       
        public Attack(Enemy owner)
        {
            this.owner = owner;
        }

        public override void Enter()
        {
            Debug.Log($"{owner.gameObject.name} is Attacking");
            owner.animator.SetBool("Attack", true);
        }

        public override void Execute()
        {
            //if (owner.PlayerIsClose())
            //{
            //    owner.behaviour.EnemyCloseAttack();
            //}
            //else if (owner.PlayerIsFar())
            //{
            //    owner.behaviour.EnemyFarAttack();
            //}
            //else
            //{
            //    owner.machine.SetState(new EndAttack(owner));
            //}
        }

        public override void Exit()
        {
            owner.animator.SetBool("Attack", false);
        }
    }
}
