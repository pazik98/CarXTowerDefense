using System;
using UnityEngine;
using System.Collections;

namespace _CarXTowerDefense.Scripts.Tower
{
	public class GuidedProjectile : Projectile {
		
		[NonSerialized] public Transform Target;
		
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

