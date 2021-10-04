using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public abstract class BaseProjectile : MonoBehaviour
    {
        private float _despawnTime;
        private MeshRenderer _meshRenderer;

        private ParticleSystem _particleSystem;
        public abstract float GetLifetimeInSeconds();
        public abstract float GetSpeed();
        public abstract float GetDegredationPerShot();
        public abstract float GetDamageAmount();
        public void Awake()
        {
            _despawnTime = Time.time + GetLifetimeInSeconds();
            _meshRenderer = GetComponentInChildren<MeshRenderer>();
            _particleSystem = GetComponentInChildren<ParticleSystem>();
        }

        public void Update()
        {
            if(Time.time > _despawnTime)
            {
                Despawn();
            }
        }

        public virtual void Despawn()
        {
            Debug.Log("Destroying bullet");
            Destroy(this.gameObject);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals("Enemy"))
            {
                var enemyMortal = other.gameObject.GetComponent<EnemyMortal>();
                float damageAmount = GetDamageAmount();
                enemyMortal.TakeDamage(damageAmount);
                Despawn();
            }
        }
    }
}
