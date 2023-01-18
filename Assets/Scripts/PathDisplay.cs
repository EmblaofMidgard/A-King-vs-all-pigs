using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDisplay : MonoBehaviour
{
    
    private void OnDrawGizmos()
    {
        
        for (int i = 0; i < transform.childCount-1; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i+1).transform.position);

            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.GetChild(i).position, 1f);
        }

        Gizmos.DrawWireSphere(transform.GetChild(transform.childCount - 1).position,1f);

    }
}
