using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltProjectile : BaseProjectile
{
    [SerializeField]
    private float _despawnTimeSeconds = 2f;

    [SerializeField]
    private float _speed = 1f;
    public override float GetLifetimeInSeconds() => _despawnTimeSeconds;
    public override float GetSpeed() => _speed;
}
