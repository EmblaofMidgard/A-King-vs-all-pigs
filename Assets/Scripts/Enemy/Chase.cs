using UnityEngine;

public partial class Enemy
{
    class Chase : IState<Enemy>
    {
        public Chase(Enemy owner)
        {
            this.owner = owner;
        }

        public override void Execute()
        {
            Debug.Log($"chase ");
            owner.transform.Translate(owner.target.position - owner.transform.position * owner.speed * Time.deltaTime);
        }
    }
}
