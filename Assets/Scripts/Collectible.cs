using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectible : MonoBehaviour
{
    public UnityEvent<Collector> CollectResponse;

    public bool oneTimeCollect = true;

    public void Collect(Collector collector)
    {
        CollectResponse?.Invoke(collector);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Collector>())
        {
            Collect(collision.gameObject.GetComponent<Collector>());
            if(oneTimeCollect)
                Destroy(this.gameObject);
        }
    }
}
