using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    [Range(1, 10)]
    public int healingValue = 1;
    public bool oneTimeHeal = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Damageable>())
        {
            if(collision.gameObject.GetComponent<Damageable>().actualHealtPoint != collision.gameObject.GetComponent<Damageable>().maxHealtPoint)
            {
                collision.gameObject.GetComponent<Damageable>().Heal(healingValue);
                if (oneTimeHeal)
                    Destroy(this.gameObject);
            }
        }
            
    }
}
