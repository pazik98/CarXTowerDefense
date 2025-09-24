using System;
using UnityEngine;

namespace _CarXTowerDefense.Scripts.Tower
{
    [RequireComponent(typeof(Collider))]
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected float speed = 0.1f;
        [SerializeField] protected float damage = 10f;
        [SerializeField] protected float lifeTime = 5f;

        private float _lifeTimer;

        private void FixedUpdate()
        {
            Move();
            if (_lifeTimer > lifeTime)
            {
                Destroy(gameObject);
            }
            else
            {
                _lifeTimer += Time.deltaTime;
            }
        }

        protected abstract void Move();
        
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