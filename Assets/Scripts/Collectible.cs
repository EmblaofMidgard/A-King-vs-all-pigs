using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ECollectibleType
{
    Diamond,
    Heart
}

public class Collectible : MonoBehaviour
{
    public bool oneTimeCollect = true;

    public ECollectibleType collectibleType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Collector>())
        {
            collision.gameObject.GetComponent<Collector>().Collect(this);
            if(oneTimeCollect)
                Destroy(this.gameObject);
        }
    }
}
