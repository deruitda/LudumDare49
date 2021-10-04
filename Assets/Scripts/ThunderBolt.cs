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
    private GameObject _bombProjectileGameObject;

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
    private GameObject _grenadeGameObject;

    [SerializeField]
    private float _grenadeStartingYPosition = 5f;

    [SerializeField]
    private float _speedOfGrenadeToss = .1f;

    [SerializeField]
    private float _meleeDistance = 3f;

    [SerializeField]
    private float _meleeShoveForce = 1000f;


    private float _overheatMetric = 0f;

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
        return Mathf.Min(_overheatMetric, 1f);
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
        if (_overheatMetric > 0)
        {
            float amountToCooldown = _cooldownPerSecond / (1 / _howOftenToCooldown);
            _overheatMetric -= Mathf.Min(amountToCooldown, _overheatMetric);

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
            FireProjectile();
            _nextShotTime = Time.time + _shotDelayInSeconds;
        }

        if(Input.GetKey(KeyCode.LeftShift) && _overheatMetric >= 1f)
        {
            //SetOffBomb
            ThrowBomb();

            //undo overheat metric
            _overheatMetric = 0f;
        }
    }

    public void ThrowBomb()
    {
        var position = transform.position;
        position.y += _grenadeStartingYPosition;
        var projectileGameObject = Instantiate(
            _grenadeGameObject,
            position,
            Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;

        projectileGameObject.GetComponent<Rigidbody>().AddForce(new Vector2(0, 1) * _speedOfGrenadeToss);
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
        _overheatMetric +=_projectile.GetDegredationPerShot();
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
