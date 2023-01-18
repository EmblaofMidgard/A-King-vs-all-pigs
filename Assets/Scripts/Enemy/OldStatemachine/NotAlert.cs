using UnityEngine;

public partial class Enemy
{
    class NotAlert : IState<Enemy>
    {
        public NotAlert(Enemy owner)
        {
            this.owner = owner;
        }

        public override void Enter()
        {
            Debug.Log($"{owner.gameObject.name} is Not Alert");
            owner.animator.SetBool("NotAlert", true);
        }

        public override void Execute()
        {
            if (owner.playerPresence == true)
                owner.machine.SetState(new Alert(owner));
            //else
            //    owner.behaviour.EnemyNotAlert();
        }

        public override void Exit()
        {
            owner.animator.SetBool("NotAlert", false);
        }

    }

}
