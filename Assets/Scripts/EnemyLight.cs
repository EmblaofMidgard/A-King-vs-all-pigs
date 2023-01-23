using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyLight : MonoBehaviour
{
    public Transform lightOfTheEnemy;
    CharacterControllerPlatformer2D player;
    Animator animator;

    public bool lightOn {private set; get;}

    public void SpegniTorcia()
    {
        SetLight(false);
        if (player != null)
            player.SetIsInLight(false);
    }

    public void AccendiTorcia()
    {
        SetLight(true);
        if (player != null)
            player.SetIsInLight(true);
    }

    private void SetLight(bool v)
    {
        lightOfTheEnemy.gameObject.SetActive(v);
        lightOn = v;
        if (animator != null)
            animator.SetBool("luce", lightOn);
    }

    public void CambiaStatoTorcia()
    {
        if (lightOn)
            SpegniTorcia();
        else
            AccendiTorcia();
    }


    private void Start()
    {
        animator = GetComponent<Animator>();
        SetTriggerRadius();
        AccendiTorcia();
    }

    private void SetTriggerRadius()
    {
        GetComponent<CircleCollider2D>().radius = GetComponentInChildren<Light2D>().pointLightOuterRadius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterControllerPlatformer2D>() == true && lightOn)
        {
            player = collision.gameObject.GetComponent<CharacterControllerPlatformer2D>();
            player.SetIsInLight(true);
        }
           
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterControllerPlatformer2D>() == true)
        {
            if (GetComponent<Enemy>())
                if (GetComponent<Enemy>().playerIsInMeleeRange)
                    return;

            collision.gameObject.GetComponent<CharacterControllerPlatformer2D>().SetIsInLight(false);
            player = null;
        }
            

    }


}
