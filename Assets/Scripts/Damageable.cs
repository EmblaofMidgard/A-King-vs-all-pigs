using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [Range(1, 10)]
    public int maxHealtPoint = 1;
    public int actualHealtPoint { get; private set; }

    public bool isAlive { get; private set; }

    public UnityEvent<Damageable> onDeath;
    public UnityEvent<Damageable> onHit;
    public UnityEvent<Damageable> onHeal;

    private void Start()
    {
        isAlive = true;
        actualHealtPoint = maxHealtPoint;
    }

    public void TakeDamage(int damage)
    {
        ModifyHealt(-damage);
        onHit?.Invoke(this);
    }

    private void ModifyHealt(int value)
    {
        actualHealtPoint += value;
        if (actualHealtPoint <= 0)
        {
            actualHealtPoint = 0;
            Die();
        }
        if (actualHealtPoint > maxHealtPoint)
            actualHealtPoint = maxHealtPoint;
    }

    private void Die()
    {
        isAlive = false;
        onDeath?.Invoke(this);
    }

    public void Heal(int healAmount)
    {
        ModifyHealt(healAmount);
        onHeal?.Invoke(this);
    }
}
