using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEye : MonoBehaviour
{
    public bool playerPresence { get; private set; }
    public Enemy enemy;

    private void Start()
    {
        playerPresence = false;
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterControllerPlatformer2D>() == true)
        {
            playerPresence = true;
            enemy.SetPlayerPresence(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterControllerPlatformer2D>() == true)
        {
            playerPresence = false;
            enemy.SetPlayerPresence(false);
        }
            
    }



}
