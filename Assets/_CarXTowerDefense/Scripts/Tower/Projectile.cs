using System;
using _CarXTowerDefense.Scripts.Pool;
using UnityEngine;

namespace _CarXTowerDefense.Scripts.Tower
{
    [RequireComponent(typeof(Collider))]
    public abstract class Projectile : MonoBehaviour, IPoolable
    {
        [SerializeField] protected float speed = 0.1f;
        [SerializeField] protected float damage = 10f;
        [SerializeField] protected float lifeTime = 5f;

        private float _lifeTimer;
        
        public float Speed => speed;
        
        protected abstract IObjectPool Pool { get; }
    
        private void FixedUpdate()
        {
            Move();
            if (_lifeTimer > lifeTime)
            {
                Despawn();
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
            Despawn();
        }

        public virtual void Spawn(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
            gameObject.SetActive(true);
        }

        public virtual void Despawn()
        {
            gameObject.SetActive(false);
            Pool.Return(this);
        }
    }
}