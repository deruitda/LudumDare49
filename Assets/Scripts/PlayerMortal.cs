using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMortal : BaseMortal
{
    public bool IsInvincible = false;
    public override void TakeDamage(float damage)
    {
        if(!IsInvincible)
        {
            base.TakeDamage(damage);
        }          
    }

    public override void Die()
    {
        _gameManager.GameOver();
        base.Die();
    }
}
