using UnityEngine;

public partial class Enemy
{
    class LightTorch : IState<Enemy>
    {
        public LightTorch(Enemy owner)
        {
            this.owner = owner;
        }

        public override void Enter()
        {
            Debug.Log($"{owner.gameObject.name} is {nameof(LightTorch)} at {Time.time}");
            owner.animator.SetBool($"{nameof(LightTorch)}", true);
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
                if (!owner.enemyLight.lightOn)
                    owner.enemyLight.AccendiTorcia();
                else
                    owner.machine.SetState(new Idle(owner));
            }
                
        }

        public override void Exit()
        {
            owner.animator.SetBool($"{nameof(LightTorch)}", false);
        }

    }

}
