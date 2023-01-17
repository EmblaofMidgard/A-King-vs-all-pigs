using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [Range(1, 10)]
    public int maxHealtPoint = 1;
    public int actualHealtPoint { get; private set; }

    public bool isAlive { get; private set; }

    private void Start()
    {
        isAlive = true;
        actualHealtPoint = maxHealtPoint;
    }

    public void TakeDamage(int damage)
    {
        ModifyHealt(-damage);
    }

    private void ModifyHealt(int value)
    {
        actualHealtPoint += value;
        if (actualHealtPoint < 0)
        {
            actualHealtPoint = 0;
            isAlive = false;
        }
        if (actualHealtPoint > maxHealtPoint)
            actualHealtPoint = maxHealtPoint;
    }
    public void Heal(int healAmount)
    {
        ModifyHealt(healAmount);
    }
}
