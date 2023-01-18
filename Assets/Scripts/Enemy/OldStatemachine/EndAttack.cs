using UnityEngine;

public partial class Enemy
{
    class EndAttack : IState<Enemy>
    {
       
        public EndAttack(Enemy owner)
        {
            this.owner = owner;
        }

        public override void Enter()
        {
            Debug.Log($"{owner.gameObject.name} is Ending Attack");
            owner.animator.SetBool("EndAttack", true);
        }

        public override void Execute()
        {
            //if (owner.PlayerIsFar())
            //    owner.machine.SetState(new Attack(owner));
            //else if (Vector3.Distance(owner.startingPoint, owner.transform.position) < owner.reachPoint)
            //    owner.machine.SetState(new NotAlert(owner));
            //else
            //owner.behaviour.EnemyEndAttack();
           
        }

        public override void Exit()
        {
            owner.animator.SetBool("EndAttack", false);
        }

    }
}
