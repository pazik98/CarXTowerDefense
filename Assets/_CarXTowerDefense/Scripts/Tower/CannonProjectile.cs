using UnityEngine;
using System.Collections;
using _CarXTowerDefense.Scripts.Tower;

namespace _CarXTowerDefense.Scripts.Tower
{
    [RequireComponent(typeof(Collider))]
    public class CannonProjectile : Projectile
    {
        public Vector3 Direction { get; set; }

        protected override void Move()
        {
            transform.Translate(Direction * (speed * Time.fixedDeltaTime), Space.Self);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<Enemy>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
