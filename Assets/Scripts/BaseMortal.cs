using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMortal : MonoBehaviour
{
    [SerializeField]
    private HealthBar _healthBar;

    public float StartingHealth = 100;
    public float Health { get; protected set; }

    public IWeapon Weapon;

    private void Awake()
    {
        Health = StartingHealth;
        _healthBar.SetHealthBarLevel(Health);
    }

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;
        _healthBar.SetHealthBarLevel(Health);
        if(Health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
    }
}
