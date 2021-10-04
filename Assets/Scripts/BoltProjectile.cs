using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltProjectile : BaseProjectile
{
    [SerializeField]
    private float _despawnTimeSeconds = 2f;

    [SerializeField]
    private float _damage = 20;

    [SerializeField]
    private float _speed = 1f;

    [SerializeField]
    private float _degredationPerShot = 0.05f;
    public override float GetLifetimeInSeconds() => _despawnTimeSeconds;
    public override float GetSpeed() => _speed;
    public override float GetDegredationPerShot() => _degredationPerShot;
    public override float GetDamageAmount() => _damage;

}
