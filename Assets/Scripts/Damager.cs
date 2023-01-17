using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [Range(1, 10)]
    public int damageDone = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Damageable>())
            collision.gameObject.GetComponent<Damageable>().TakeDamage(damageDone);
    }
}
