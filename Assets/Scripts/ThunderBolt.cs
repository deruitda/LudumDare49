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
    private Transform _shoulder;

    [SerializeField]
    private Transform _player;

    [SerializeField]
    private float _cooldownPerSecond = .05f;

    [SerializeField]
    private float _howOftenToCooldown = 0.01f;

    [SerializeField]
    private float _meleeDistance = 3f;

    [SerializeField]
    private float _meleeShoveForce = 1000f;


    private float _overheatMetric = 0f;
    private bool _isOverheated = false;

    private float _nextShotTime = 0.0f;
    float tempTime;
    private Vector3 _meleeHeightOffset = new Vector3(0, 2, 0);

    // Update is called once per frame
    void FixedUpdate()
    {
        Attack();
        AttemptCooldown();
    }

    public float GetOverheatMetric()
    {
        return _overheatMetric;
    }

    private void AttemptCooldown()
    {
        tempTime += Time.deltaTime;
        if (tempTime > _howOftenToCooldown)
        {
            tempTime = 0;
            Cooldown();
        }
    }

    private void Cooldown()
    {
        if(_overheatMetric > 0)
        {
            float amountToCooldown = _cooldownPerSecond / (1 / _howOftenToCooldown);
            _overheatMetric -= Mathf.Min(amountToCooldown, _overheatMetric);

        }
        if(_isOverheated && _overheatMetric == 0f)
        {
            _isOverheated = false;
        }
        
    }
    private float getMouseAngle()
    {
        return (PlayerAim.GetMouseAngle(_shoulder) * Mathf.Rad2Deg) + 90;
    }


    public void Attack()
    {
        // fire projectile
        if (Input.GetButton("Fire1") && Time.time > _nextShotTime)
        {
            if (!_isOverheated) {
                FireProjectile();
                _nextShotTime = Time.time + _shotDelayInSeconds;
            }
        }

        if(Input.GetMouseButton(1))
        {
            PerformMelee();
        }
    }

    private void PerformMelee()
    {
        var right = new Vector3(_meleeDistance, _meleeHeightOffset.y, 0);
        Debug.DrawLine(_player.position + _meleeHeightOffset, _player.position + right, Color.red);

        if(Physics.Raycast(_player.position + _meleeHeightOffset, Vector2.right, out var hit, _meleeDistance))
        {
            Debug.Log("Enemy hit");          
            var enemy = hit.collider.gameObject;

            if(enemy.tag.Equals("Enemy"))
            {
                var mortal = enemy.GetComponent<EnemyMortal>();
                mortal.ReceiveMelee(5, right * _meleeShoveForce);
            }
        }

        
    }

    private void CreateProjectile()
    {

        float angle = getMouseAngle();

        var projectileGameObject = Instantiate(
            _projectileGameObject,
            GetProjectilePosition(),
            Quaternion.Euler(new Vector3(0, 0, angle))) as GameObject;

        projectileGameObject.GetComponent<Rigidbody>().AddForce((transform.up) * _projectile.GetSpeed());
    }
    private void FireProjectile()
    {
        CreateProjectile();
        _projectileAudio.Play();
        ApplyProjectileHeat();
    }

    private void ApplyProjectileHeat()
    {
        _overheatMetric += Mathf.Min(_projectile.GetDegredationPerShot(), 1f - _overheatMetric);
        if(_overheatMetric >= 1f)
        {
            _isOverheated = true;
        }

    }

    private float getProjectileSpawnOffset()
    {
        return Mathf.Max(0.3f, _projectileSpawnOffset);
    }

    private Vector2 GetProjectilePosition()
    {
        float angle = PlayerAim.GetMouseAngle(_shoulder);


        float projectileSpawnOffset = getProjectileSpawnOffset();
        float xPos = Mathf.Cos(angle) * (transform.localScale.y + projectileSpawnOffset);
        float yPos = Mathf.Sin(angle) * (transform.localScale.y + projectileSpawnOffset);

        Vector2 v = new Vector2(transform.position.x + xPos, transform.position.y + +yPos);
        return v;
    }
}
