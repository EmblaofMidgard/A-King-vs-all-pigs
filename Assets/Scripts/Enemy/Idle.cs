using UnityEngine;

public partial class Enemy
{
    class Idle : IState<Enemy>
    {
        public Idle(Enemy owner)
        {
            this.owner = owner;
        }

        public override void Enter()
        {
            Debug.Log($"{owner.gameObject.name} is Idle");
        }
        public override void Execute()
        {
            if (owner.playerPresence == true)
                owner.machine.SetState(new Idle(owner));//da modificare

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterControllerPlatformer2D>() == true)
            playerPresence = true;
    }


}
