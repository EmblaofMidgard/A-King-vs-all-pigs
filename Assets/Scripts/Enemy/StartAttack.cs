using UnityEngine;

public partial class Enemy
{
    class StartAttack : IState<Enemy>
    {
        bool doneFirstAttack;
        
        public StartAttack(Enemy owner)
        {
            this.owner = owner;
            doneFirstAttack = false;
        }

        public override void Enter()
        {
            Debug.Log($"{owner.gameObject.name} Start Attack");
            owner.animator.SetBool("StartAttack", true);
        }

        public override void Execute()
        {
            if (!doneFirstAttack)
            {
                owner.behaviour.EnemyStartAttack();
                doneFirstAttack = true;
            }
            else
                owner.machine.SetState(new Attack(owner));

        }

        public override void Exit()
        {
            owner.animator.SetBool("StartAttack", false);
        }

    }
}
