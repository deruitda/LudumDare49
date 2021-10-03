using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBolt : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectileGameObject;

    [SerializeField]
    private float _shotDelayInSeconds = 3;

    [SerializeField]
    private float _projectileSpawnOffset = 2;

    [SerializeField]
    private BaseProjectile _projectile;

    [SerializeField]
    private AudioSource _projectileAudio;

    [SerializeField]
    private Transform _player;

    private float _nextShotTime = 0.0f; 

    // Update is called once per frame
    void FixedUpdate()
    {
        AimGun();
        Attack();
    }
    private float getMouseAngle()
    {
        return (PlayerAim.GetMouseAngle(_player) * Mathf.Rad2Deg) + 90;
    }
    public void AimGun()
    {
        float angle = getMouseAngle();
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
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

    private float getProjectileSpawnOffset()
    {
        return Mathf.Max(0.3f, _projectileSpawnOffset);
    }

    private Vector2 GetProjectilePosition()
    {
        float angle = PlayerAim.GetMouseAngle(_player);


        float projectileSpawnOffset = getProjectileSpawnOffset();
        float xPos = Mathf.Cos(angle) * (transform.localScale.y + projectileSpawnOffset);
        float yPos = Mathf.Sin(angle) * (transform.localScale.y + projectileSpawnOffset);

        Vector2 v = new Vector2(transform.position.x + xPos, transform.position.y + +yPos);
        return v;
    }
}
