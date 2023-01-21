using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDisplay : MonoBehaviour
{
    public float dotSize = 0.2f;


    private void OnDrawGizmos()
    {
        
        for (int i = 0; i < transform.childCount-1; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i+1).transform.position);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.GetChild(i).position, dotSize);
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.GetChild(transform.childCount - 1).position, dotSize);

    }
}
