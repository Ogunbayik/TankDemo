using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    public event Action<int> OnTakeDamage;
    public event Action OnDeath;

    [SerializeField] private int maxHealth;

    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if(currentHealth > damage)
        {
            currentHealth -= damage;
            OnTakeDamage?.Invoke(damage);
        }
        else
        {
            currentHealth = 0;
            OnDeath?.Invoke();
        }


    }
}
