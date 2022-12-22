using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class EnemyLight : MonoBehaviour
{
    public Transform light;
    public float lightOnDelay;


    Animator animator;

    float time;

    bool luce;


    public void SpegniTorcia()
    {
        light.gameObject.SetActive(false);
        luce = false;
    }

    public void AccendiTorcia()
    {
        light.gameObject.SetActive(true);
        luce = true;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (!light.gameObject.activeSelf)
            time = lightOnDelay;
    }


    private void Update()
    {

        if (!luce)
            time = lightOnDelay;


        if (time > 0)
        {
            time -= Time.deltaTime;

            if (time <= 0)
                luce = true;
        }



        animator.SetBool("luce", luce);
    }

}
