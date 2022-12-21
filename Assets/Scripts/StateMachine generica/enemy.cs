using System.Collections;
using System.Collections.Generic;
using UnityEngine;







public class enemy : MonoBehaviour
{



    class Idle : IState<enemy>
    {
        public Idle(enemy owner)
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

    class Chase : IState<enemy>
    {
        public Chase(enemy owner)
        {
            this.owner = owner;
        }

        public override void Execute()
        {
            Debug.Log($"chase ");
            owner.transform.Translate(owner.target.position - owner.transform.position * owner.speed * Time.deltaTime);
        }
    }



    public StateMachine<enemy> machine;

    public Transform target;

    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        machine = new StateMachine<enemy>(new Idle(this));
    }

    // Update is called once per frame
    void Update()
    {
        machine.StateUpdate();
        
    }
}
