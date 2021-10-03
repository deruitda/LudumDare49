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
}
