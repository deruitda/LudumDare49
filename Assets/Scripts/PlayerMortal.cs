using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMortal : BaseMortal
{
    public override void Die()
    {
        _gameManager.GameOver();
        base.Die();
    }
}
