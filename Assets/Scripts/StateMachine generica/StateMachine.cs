using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class IState<T>
{
    public T owner;
    public virtual void Enter() { }
    public virtual void Execute() { }
    public virtual void Exit() { }

}

public class StateMachine<T> 
{
    IState<T> statoCorrente;


    public StateMachine(IState<T> statoDefault)
    {
        SetState(statoDefault);
    }


    public void StateUpdate()
    {
        if (statoCorrente != null)
            statoCorrente.Execute();
    }

    public void SetState(IState<T> nuovoStato)
    {
        if(statoCorrente!=null)
            statoCorrente.Exit();

        statoCorrente = nuovoStato;

        if (statoCorrente != null)
            statoCorrente.Enter();
        
    }
    
}
