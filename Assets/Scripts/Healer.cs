using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    [Range(1, 10)]
    public int healingValue = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Damageable>())
        {
            collision.gameObject.GetComponent<Damageable>().Heal(healingValue); 
        }
            
    }

    //public void HealCollector(Collector collector)
    //{
    //    if (collector.gameObject.GetComponent<Damageable>() != null)
    //    {
    //        collector.gameObject.GetComponent<Damageable>().Heal(healingValue);
    //    }
    //}
}
