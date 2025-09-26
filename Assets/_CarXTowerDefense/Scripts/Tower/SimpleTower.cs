using UnityEngine;
using System.Collections;
using _CarXTowerDefense.Scripts.Tower;

namespace _CarXTowerDefense.Scripts.Tower
{
	public class SimpleTower : Tower 
	{
		[SerializeField] private GuidedProjectile projectilePrefab;
		
		protected override void Shoot()
		{
			var projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
			projectile.Target = Target.transform;
		}
	}
}

