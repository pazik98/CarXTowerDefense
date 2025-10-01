using System;
using UnityEngine;

namespace _CarXTowerDefense.Scripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float health = 100f;
        [SerializeField] private float speed = 3f;
        [SerializeField] private float lifetime = 20f;

        private float _lifeTimer;
        
        private Rigidbody _rigidbody;
        
        public Vector3 Velocity => _rigidbody.linearVelocity;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            _rigidbody.linearVelocity = transform.TransformDirection(Vector3.forward) * speed;
            _lifeTimer += Time.fixedDeltaTime;
            if (_lifeTimer >= lifetime)
            {
                Destroy(gameObject);
            }
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}