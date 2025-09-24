using System;
using R3;
using UnityEngine;
using Zenject;

namespace _CarXTowerDefense.Scripts.Tower
{
    public abstract class Tower : MonoBehaviour
    {
        [SerializeField] protected Transform center;
        
        [Header("Shooting")]
        [SerializeField] protected float shootInterval = 1f;
        [SerializeField] protected Transform shootPoint;
        [SerializeField] protected float shootRange = 5f;

        [Header("Detection")] 
        [SerializeField] protected float detectionRange = 5f;
        [SerializeField] protected float detectionInterval = 0.05f;
        [SerializeField] protected LayerMask enemyLayerMask;
        
        protected readonly Collider[] DetectedCollidersBuffer = new Collider[32];
        protected Transform Target;

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
            else if (Target != null)
            {
                Shoot();
                _shootTimer = shootInterval;
            }
        }
        
        protected virtual void Detect()
        {
            Array.Clear(DetectedCollidersBuffer, 0, DetectedCollidersBuffer.Length);
            Physics.OverlapSphereNonAlloc(center.position, detectionRange, DetectedCollidersBuffer, enemyLayerMask);
            
            if (Target != null && Vector3.Distance(Target.position, center.position) > detectionRange)
            {
                Target = null;
            }
            
            if (Target == null && DetectedCollidersBuffer[0] != null)
            {
                Target = DetectedCollidersBuffer[0].transform;
            }
        }
        
        protected abstract void Shoot();
        
        private void OnDestroy()
        {
            CancelInvoke(nameof(Detect));
        }
    }
}