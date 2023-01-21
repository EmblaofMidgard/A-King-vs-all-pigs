using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyLight : MonoBehaviour
{
    public Transform lightOfTheEnemy;

    Animator animator;

    public bool lightOn {private set; get;}

    public void SpegniTorcia()
    {
        SetLight(false);
    }

    public void AccendiTorcia()
    {
        SetLight(true);
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
            collision.gameObject.GetComponent<CharacterControllerPlatformer2D>().SetIsInLight(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterControllerPlatformer2D>() == true)
            collision.gameObject.GetComponent<CharacterControllerPlatformer2D>().SetIsInLight(false);

    }


}
