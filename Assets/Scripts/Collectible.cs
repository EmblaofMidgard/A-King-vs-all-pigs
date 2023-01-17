using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectible : MonoBehaviour
{
    public UnityEvent CollectResponse;

    public void Collect()
    {
        CollectResponse?.Invoke();
    }
}
