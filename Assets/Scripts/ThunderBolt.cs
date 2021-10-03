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
    void Update()
    {
        AimGun();
        Attack();
    }

    public void AimGun()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -0.639f;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        //angle = Mathf.Clamp(Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg, -1 * _clampAngle, _clampAngle);

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
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
