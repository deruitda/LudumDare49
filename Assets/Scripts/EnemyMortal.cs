using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMortal : BaseMortal
{
    [SerializeField]
    private AudioSource _painAudio;

    [SerializeField]
    private AudioSource _deathAudio;

    [SerializeField]
    private AudioSource _meleeAudio;

    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private Collider _collider;

    [SerializeField]
    private EnemyAi _enemyAi;

    public EnemySpawner Spawner;


    public override void TakeDamage(float damage)
    {
        _painAudio.Play();
        base.TakeDamage(damage);
    }

    public override void Die()
    {
        IsDead = true;
        _painAudio.Stop();
        _deathAudio.Play();
        _enemyAi.Stop();
        Destroy(_rigidbody);
        Destroy(_collider);
        Spawner.RecordDeadEnemy();
        Invoke("DieDelayed", 1f);
    }

    private void DieDelayed()
    {
        base.Die();
    }

    public void ReceiveMelee(float damage, Vector2 force)
    {
        TakeDamage(damage);
        _rigidbody.AddForce(force);
        _meleeAudio.Play();

        Invoke("ResetVelocity", .5f);
    }

    private void ResetVelocity()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }
}
