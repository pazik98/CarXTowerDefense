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
        [SerializeField] protected float detectionRange = 5f;
        [SerializeField] protected float detectionInterval = 0.05f;
        [SerializeField] protected LayerMask enemyLayerMask;

        protected Transform Target;
        
        private readonly Collider[] _detectedCollidersBuffer = new Collider[32];
        private float _detectionTimer;
        private float _shootTimer;

        private void Start()
        {
            _detectionTimer = detectionInterval;
            _shootTimer = shootInterval;
        }

        private void FixedUpdate()
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
            else if (Target != null && Vector3.Distance(shootPoint.position, Target.position) <= shootRange)
            {
                Shoot();
                _shootTimer = shootInterval;
            }
        }
        
        protected virtual void Detect()
        {
            Array.Clear(_detectedCollidersBuffer, 0, _detectedCollidersBuffer.Length);
            Physics.OverlapSphereNonAlloc(center.position, detectionRange, _detectedCollidersBuffer, enemyLayerMask);
            
            if (Target != null && Vector3.Distance(Target.position, center.position) > detectionRange)
            {
                Target = null;
            }
            
            if (Target == null && _detectedCollidersBuffer[0] != null)
            {
                Target = _detectedCollidersBuffer[0].transform;
            }
        }
        
        protected abstract void Shoot();
    }
}