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

    [SerializeField]
    private float _clampAngle = 75f;

    [SerializeField]
    private float weaponHoldingX = 0.736f;

    [SerializeField]
    private float weaponHoldingY = 0.197f;

    [SerializeField]
    private Transform player;

    private float _nextShotTime = 0.0f; 

    // Update is called once per frame
    void FixedUpdate()
    {
        //AimGun();
        Attack();
    }
    private float getMouseAngle()
    {
        return PlayerAim.GetMouseAngle(transform);
    }
    public void AimGun()
    {
        float angle = getMouseAngle();
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
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
        float angle = getMouseAngle();

        var projectileGameObject = Instantiate(
            _projectileGameObject,
            GetProjectilePosition(),
            Quaternion.Euler(new Vector3(0, 0, angle))) as GameObject;

        projectileGameObject.GetComponent<Rigidbody>().AddForce((-transform.up) * _projectile.GetSpeed());
    }

    private Vector2 GetProjectilePosition() => new Vector2(transform.position.x + _projectileSpawnOffsetX, transform.position.y + _projectileSpawnOffsetY);
}
