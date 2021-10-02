using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBolt : MonoBehaviour, IWeapon
{
    [SerializeField]
    private GameObject _projectileGameObject;

    [SerializeField]
    private float _shotDelayInSeconds = 3;

    [SerializeField]
    private float _projectileSpawnOffsetX = 2;

    [SerializeField] 
    float _projectileSpawnOffsetY = 0;

    [SerializeField]
    private BaseProjectile _projectile;

    [SerializeField]
    private AudioSource _projectileAudio;

    private float _nextShotTime = 0.0f; 

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    public void Attack()
    {
        // fire projectile
        if (Input.GetButton("Fire1") && Time.time > _nextShotTime)
        {
            FireProjectile();
            _nextShotTime = Time.time + _shotDelayInSeconds;
        }
    }

    private void FireProjectile()
    {
        var aim = PlayerAim.GetRelativeAim(transform);
        Debug.DrawLine(transform.position, aim, Color.red);

        var projectileGameObject = Instantiate(
            _projectileGameObject,
            GetProjectilePosition(),
            Quaternion.Euler(new Vector3(0, 0, 90))) as GameObject;

        projectileGameObject.GetComponent<Rigidbody>().AddForce((-transform.up) * _projectile.GetSpeed());
    }

    private Vector2 GetProjectilePosition() => new Vector2(transform.position.x + _projectileSpawnOffsetX, transform.position.y + _projectileSpawnOffsetY);
}
