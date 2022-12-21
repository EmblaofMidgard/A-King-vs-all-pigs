using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour
{

    public float range=1f;
    public LayerMask interactionMask;

    public void TryInteract()
    {
        Collider2D[] result= Physics2D.OverlapCircleAll(transform.position, range, interactionMask);

        foreach (Collider2D coll in result)
        {
            Interactable interactable = coll.GetComponent<Interactable>();
            if (interactable != null)
                interactable.Interact();
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
