using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMortal : BaseMortal
{
    [SerializeField]
    private AudioSource _painAudio;

    [SerializeField]
    private AudioSource _deathAudio;
    public override void TakeDamage(float damage)
    {
        _painAudio.Play();
        base.TakeDamage(damage);
    }

    public override void Die()
    {
        _painAudio.Stop();
        _deathAudio.Play();
        Invoke("DieDelayed", 1f);
    }

    private void DieDelayed()
    {
        base.Die();
    }
}
