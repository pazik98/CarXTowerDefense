using System;
using UnityEngine;
using System.Collections;
using _CarXTowerDefense.Scripts.Pool;

namespace _CarXTowerDefense.Scripts.Tower
{
	public class GuidedProjectile : Projectile {

		public Transform Target { get; set; }

		protected override IObjectPool Pool => PoolManager.Instance.GuidedProjectilePool;

		protected override void Move()
		{
			if (Target != null)
			{
				var direction = (Target.position - transform.position).normalized;
				transform.Translate(direction * (speed * Time.fixedDeltaTime));
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}
}

