using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMortal : MonoBehaviour
{
    [SerializeField]
    protected GameManager _gameManager;

    [SerializeField]
    private HealthBar _healthBar;

    public float StartingHealth = 100;
    public float Health { get; protected set; }

    public BaseWeapon Weapon;

    public bool IsDead { get; protected set; }

    public void Awake()
    {
        Health = StartingHealth;
        _healthBar.SetHealthBarLevel(Health);
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
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
        IsDead = true;
        Destroy(this.gameObject);
    }
}
