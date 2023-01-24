using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collector : MonoBehaviour
{
    public UIManager uiManager;
    
    public UnityEvent<Collectible> OnCollect;

    public void Collect(Collectible collectible)
    {
        OnCollect?.Invoke(collectible);
    }
}
