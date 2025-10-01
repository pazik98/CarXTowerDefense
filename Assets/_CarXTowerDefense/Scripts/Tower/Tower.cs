using System;
using R3;
using UnityEngine;
using Zenject;

namespace _CarXTowerDefense.Scripts.Tower
{
    public abstract class Tower : MonoBehaviour
    {
        [Header("Shooting")]
        [SerializeField] protected Transform shootPoint;
        [SerializeField] protected float shootInterval = 1f;
        [SerializeField] protected float shootRange = 5f;

        [Header("Detection")]
        [SerializeField] protected Transform center;
        [SerializeField] protected float detectionInterval = 0.05f;
        [SerializeField] protected LayerMask enemyLayerMask;

        protected Enemy Target;
        
        private readonly Collider[] _detectedCollidersBuffer = new Collider[32];
        private float _detectionTimer;
        private float _shootTimer;

        protected virtual bool IsInShootRange => Vector3.Distance(center.position, Target.transform.position) <= shootRange;
        protected virtual bool CanShoot => IsInShootRange;

        private void Start()
        {
            _detectionTimer = detectionInterval;
            _shootTimer = shootInterval;
        }

        private void FixedUpdate()
        {
            Tick();
        }

        protected virtual void Tick()
        {
            if (_detectionTimer > 0)
            {
                _detectionTimer -= Time.fixedDeltaTime;
            }
            else
            {
                Detect();
                _detectionTimer = detectionInterval;
            }

            if (_shootTimer > 0)
            {
                _shootTimer -= Time.fixedDeltaTime;
            }
            else if (Target != null && CanShoot)
            {
                Shoot();
                _shootTimer = shootInterval;
            }
        }
        
        protected virtual void Detect()
        {
            Array.Clear(_detectedCollidersBuffer, 0, _detectedCollidersBuffer.Length);
            Physics.OverlapSphereNonAlloc(center.position, shootRange, _detectedCollidersBuffer, enemyLayerMask);
            
            if (Target != null && Vector3.Distance(Target.transform.position, center.position) > shootRange)
            {
                OnTargetLost();
            }
            
            if (Target == null && _detectedCollidersBuffer[0] != null)
            {
                OnTargetDetected(_detectedCollidersBuffer[0].GetComponent<Enemy>());
            }
        }

        protected abstract void Shoot();

        protected virtual void OnTargetDetected(Enemy enemy)
        {
            Target = enemy;
        }

        protected virtual void OnTargetLost()
        {
            Target = null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(center.position, shootRange);
        }
    }
}