using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 0f;

    private float health;
    public float currentHealth { get { return health; } }

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Max(health, 0);
    }

    public void AddHealth(float amount)
    {
        health += amount;
        health = Mathf.Min(health, maxHealth);
    }
}