using System;
using UnityEngine;
using System.Collections;

namespace _CarXTowerDefense.Scripts.Tower
{
	public class CannonTower : Tower {
		
		[Header("Cannon")]
		[SerializeField] private CannonProjectile projectilePrefab;
		[SerializeField] private Transform cannonHubTransform;
		[SerializeField] private Transform cannonTransform;

		private void Update()
		{
			if (Target == null)
				return;
				
			var targetPosition = Target.position;
			var direction = (targetPosition - shootPoint.position).normalized;

			cannonHubTransform.localRotation = Quaternion.LookRotation(direction);
			cannonHubTransform.localEulerAngles = new Vector3(0f, cannonHubTransform.eulerAngles.y, 0f);
			
			cannonTransform.localRotation = Quaternion.LookRotation(direction);
			cannonTransform.localEulerAngles = new Vector3(cannonTransform.eulerAngles.x, 0f, 0f);
		}

		protected override void Shoot()
		{
			if (Vector3.Distance(shootPoint.position, Target.position) > shootRange)
			{
				return;
			}
			
			var projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation); //TODO Pool?
			projectile.Direction = Vector3.forward;
		}
		
	}
}

