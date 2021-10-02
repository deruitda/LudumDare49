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
        public abstract float GetLifetimeInSeconds();
        public abstract float GetSpeed();

        public void Awake()
        {
            _despawnTime = Time.time + GetLifetimeInSeconds();
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
    }
}
