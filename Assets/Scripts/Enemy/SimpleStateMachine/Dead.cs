using UnityEngine;

public partial class Enemy
{
    class Dead : IState<Enemy>
    {
       
        public Dead(Enemy owner)
        {
            this.owner = owner;
        }

        public override void Enter()
        {
            Debug.Log($"{owner.gameObject.name} is {nameof(Dead)} at {Time.time}");
            owner.animator.SetBool($"{nameof(Dead)}", true);
            owner.elapsed = 0f;
        }

        public override void Execute()
        {

            if (owner.elapsed > owner.deadCorpseRemainTime)
                Destroy(owner.gameObject);
            else
                owner.elapsed += Time.deltaTime;
            
                
        }

        public override void Exit()
        {
            owner.animator.SetBool($"{nameof(Dead)}", false);
            owner.elapsed = 0f;
        }

    }

}
