using UnityEngine;
using System.Collections;
using _CarXTowerDefense.Scripts.Pool;
using _CarXTowerDefense.Scripts.Tower;

namespace _CarXTowerDefense.Scripts.Tower
{
	public class CrystalTower : Tower 
	{
		[SerializeField] private GuidedProjectile projectilePrefab;

		private GuidedProjectilePool _projectilePool;

		protected override void Start()
		{
			base.Start();
			_projectilePool	= PoolManager.Instance.GuidedProjectilePool;
		}

		protected override void Shoot()
		{
			var projectile = (GuidedProjectile)_projectilePool.Get(shootPoint.position, Quaternion.identity);
			projectile.Target = Target.transform;
		}
	}
}

