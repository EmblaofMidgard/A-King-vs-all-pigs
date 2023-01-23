using UnityEngine;

public partial class Enemy
{
    class LightTorch : IState<Enemy>
    {
        float delayLight = 2f;
        float elapsed;
        
        public LightTorch(Enemy owner)
        {
            this.owner = owner;
        }

        public override void Enter()
        {
            Debug.Log($"{owner.gameObject.name} is {nameof(LightTorch)} at {Time.time}");
            owner.animator.SetTrigger($"{nameof(LightTorch)}");
            elapsed = 0f;
            owner.isInLightTorch = true;
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
                {
                    if (elapsed > delayLight)
                        owner.enemyLight.AccendiTorcia();
                    else
                        elapsed += Time.deltaTime;
                }
                else
                    owner.machine.SetState(new Idle(owner));
            }
                
        }

        public override void Exit()
        {
            
            owner.isInLightTorch = false;
        }

    }

}
