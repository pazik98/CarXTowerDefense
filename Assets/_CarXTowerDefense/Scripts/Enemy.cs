using System;
using UnityEngine;

namespace _CarXTowerDefense.Scripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float health = 100f;

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