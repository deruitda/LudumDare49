using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GrenadeProjectile _grenadeProjectile;

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
        float angle = 0f;
        Vector2 projectilePosition = transform.position + new Vector3(2, 0);

        var projectileGameObject = Instantiate(
            _grenadeProjectileGameObject,
            projectilePosition,
            Quaternion.Euler(new Vector3(0, 0, angle))) as GameObject;

        projectileGameObject.GetComponent<Rigidbody>().AddForce((transform.up) * _grenadeProjectile.GetSpeed());
    }
    private void OnTriggerEnter(Collider other)
    {
        //Explode();
    }
}
