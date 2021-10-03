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
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //var aim = PlayerAim.GetMouseWorldPosition();
        //var angle = Mathf.Atan2(aim.x - transform.position.x, aim.y - transform.position.y);
        //transform.Rotate(Vector3.up, angle);
        //float horizontalAxis = Mathf.Cos(angle);
        //float verticalAxis = Mathf.Sin(angle);

        //Vector3 endPosition = startPosition;
        //endPosition.x += (horizontalAxis + lineMultiplier);
        //endPosition.y += (verticalAxis + lineMultiplier);

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
