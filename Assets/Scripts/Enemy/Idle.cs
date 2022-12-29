using UnityEngine;

public partial class Enemy
{
    class Idle : IState<Enemy>
    {
        public Idle(Enemy owner)
        {
            this.owner = owner;
        }

        bool tempo;

        float time;

        public override void Enter()
        {
            tempo = false;
            time = 10;
        }
        public override void Execute()
        {
            Debug.Log($"idle{time} ");
            //codice dello stato
            if (time < 0)
            {
                tempo = true;
                
            }
            else
                time -= Time.deltaTime;


            //se condizione di passsaggio in stato chase
            if (tempo)
                owner.machine.SetState(new Chase(owner));


        }
    }
}
