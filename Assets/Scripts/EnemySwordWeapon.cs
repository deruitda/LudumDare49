using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordWeapon : BaseWeapon
{
    public float Damage = 10;

    public override float GetWeaponDamage() => Damage;
}
