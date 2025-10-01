using System;
using UnityEngine;

namespace _CarXTowerDefense.Scripts.Pool
{
    public class PoolManager : MonoBehaviour
    {
        [SerializeField] private CannonProjectilePool cannonProjectilePool;
        [SerializeField] private GuidedProjectilePool guidedProjectilePool;
        [SerializeField] private EnemyPool enemyPool;
        
        public static PoolManager Instance { get; private set; }
        public CannonProjectilePool CannonProjectilePool => cannonProjectilePool;
        public GuidedProjectilePool GuidedProjectilePool => guidedProjectilePool;
        public EnemyPool EnemyPool => enemyPool;

        private void Awake()
        {
            Instance = this;
            if (cannonProjectilePool == null) cannonProjectilePool = null;
        }
    }
}