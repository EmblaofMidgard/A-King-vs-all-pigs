using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class IState
{
    public virtual void Enter() { }
    public virtual void Execute() { }
    public virtual void Exit() { }

}

public class StateMachine 
{
    IState statoCorrente;


    public StateMachine(IState statoDefault)
    {
        SetState(statoDefault);
    }


    public void StateUpdate()
    {
        if (statoCorrente != null)
            statoCorrente.Execute();
    }

    public void SetState(IState nuovoStato)
    {
        if(statoCorrente!=null)
            statoCorrente.Exit();

        statoCorrente = nuovoStato;

        if (statoCorrente != null)
            statoCorrente.Enter();
        
    }
    
}
