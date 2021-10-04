using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GrenadeProjectile _grenadeProjectile;

    [SerializeField]
    private float _projectileSpawnOffset = 2;

    [SerializeField]
    private int _numberOfProjectiles = 50;

    [SerializeField]
    private GameObject _grenadeProjectileGameObject;

    private Rigidbody _grenadeRB;
    private bool _exploded = false;
    void Start()
    {

        _grenadeRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        bool ySpeedIsZero = YSpeedIsZero();
        if(ySpeedIsZero)
        {
            Explode();
        }
    }

    private bool YSpeedIsZero()
    {
        return _grenadeRB.velocity.y <= 0;
    }

    private void Explode()
    {
        if(_exploded == false)
        {
            _exploded = true;
            ShootProjectiles();
            DestroyGrenade();
        }
    }
    private void DestroyGrenade()
    {
        Destroy(gameObject);
    }
    private void ShootProjectiles()
    {
        //float projectileRadius = _grenadeProjectile.GetRadius();
        float grenadeRadius = GetComponent<SphereCollider>().radius;
        float additionalAngle = _numberOfProjectiles / (Mathf.PI * 2);
        float angle = 0f;
        for (var i = 0; i < _numberOfProjectiles; i++)
        {
            float projectileSpawnOffset = getProjectileSpawnOffset();
            float xPos = Mathf.Cos(angle) * (transform.localScale.y + projectileSpawnOffset);
            float yPos = Mathf.Sin(angle) * (transform.localScale.y + projectileSpawnOffset);

            Vector3 v = new Vector3(transform.position.x + xPos, transform.position.y + +yPos, 0);

            var projectileGameObject = Instantiate(
                _grenadeProjectileGameObject,
                v,
                Quaternion.Euler(new Vector3(0, 0, (angle * Mathf.Rad2Deg) + 90))) as GameObject;

            Vector3 initialForce = new Vector3(xPos, yPos);

            projectileGameObject.GetComponent<Rigidbody>().AddForce(initialForce * _grenadeProjectile.GetSpeed());
            angle += additionalAngle;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        //Explode();
    }


    private float getProjectileSpawnOffset()
    {
        return Mathf.Max(0.3f, _projectileSpawnOffset);
    }
}
