using UnityEngine;
using System.Collections;
using _CarXTowerDefense.Scripts.Pool;
using _CarXTowerDefense.Scripts.Tower;

namespace _CarXTowerDefense.Scripts.Tower
{
    public class CannonProjectile : Projectile
    {
        [SerializeField] protected float gravity = 9.8f;
        
        public float Gravity => gravity;

        public override IObjectPool Pool => PoolManager.Instance.CannonProjectilePool;

        protected override void Move()
        {
            var horizontalVector = Vector3.forward * (speed * Time.fixedDeltaTime);
            transform.Translate(horizontalVector, Space.Self);
            var verticalVector = Vector3.down * (gravity * Time.fixedDeltaTime);
            transform.Translate(verticalVector, Space.World);
        }
    }
}
