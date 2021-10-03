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
            _meshRenderer.enabled = false;
            _particleSystem.Stop();
            // need to execute with a delay here so that the sound continues to play
            Invoke("DestroySelf", 1);
        }

        private void DestroySelf()
        {
            Destroy(this.gameObject);
        }
    }
}
