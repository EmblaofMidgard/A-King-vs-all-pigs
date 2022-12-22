using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class EnemyLight : MonoBehaviour
{
    public Transform lightOfTheEnemy;
    public float lightOnDelay;


    Animator animator;

    float time;

    bool luce;


    public void SpegniTorcia()
    {
        lightOfTheEnemy.gameObject.SetActive(false);
        luce = false;
    }

    public void AccendiTorcia()
    {
        lightOfTheEnemy.gameObject.SetActive(true);
        luce = true;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (!lightOfTheEnemy.gameObject.activeSelf)
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
