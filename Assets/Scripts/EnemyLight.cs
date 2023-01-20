using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Animator))]
public class EnemyLight : MonoBehaviour
{
    public Transform lightOfTheEnemy;
    public float lightOnDelay;

    Animator animator;

    float time;

    public bool lightOn {private set; get;}

    public void SpegniTorcia()
    {
        lightOfTheEnemy.gameObject.SetActive(false);
        lightOn = false;
    }

    public void AccendiTorcia()
    {
        lightOfTheEnemy.gameObject.SetActive(true);
        lightOn = true;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        SetTriggerRadius();

        if (!lightOfTheEnemy.gameObject.activeSelf)
            time = lightOnDelay;
    }

    private void SetTriggerRadius()
    {
        GetComponent<CircleCollider2D>().radius = GetComponentInChildren<Light2D>().pointLightOuterRadius;
    }

    private void Update()
    {

        if (!lightOn)
            time = lightOnDelay;


        if (time > 0)
        {
            time -= Time.deltaTime;

            if (time <= 0)
                lightOn = true;
        }

        animator.SetBool("luce", lightOn);
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
