using UnityEngine;

public partial class Enemy
{
    class Alert : IState<Enemy>
    {
        public Alert(Enemy owner)
        {
            this.owner = owner;
        }

        public override void Enter()
        {
            Debug.Log($"{owner.gameObject.name} is Alert");
            owner.animator.SetBool("Alert", true);
        }

        public override void Execute()
        {
            if (owner.playerSpotted == true)
                owner.machine.SetState(new StartAttack(owner));
            else if(owner.playerPresence == false)
                owner.machine.SetState(new NotAlert(owner));
            //else
            //    owner.behaviour.EnemyAlert();
        }

        public override void Exit()
        {
            owner.animator.SetBool("Alert", false);
        }
    }
}
