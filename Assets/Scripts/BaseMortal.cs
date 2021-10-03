using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMortal : MonoBehaviour
{
    public float StartingHealth = 100;
    public float Health { get; protected set; }

    public IWeapon Weapon;
    private void Awake()
    {
        Health = StartingHealth;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
