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

		private bool _isAimed;
		
		protected override bool CanShoot => base.CanShoot && _isAimed;

		private void Update()
		{
			_isAimed = false;
			
			if (Target == null)
				return;
			
			AimTower();
			_isAimed = true;
		}

		protected override void Shoot()
		{
			Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation); //TODO Pool
		}

		private void AimTower()
		{
			var targetPosition = CalculatePrediction(3);
			var direction = (targetPosition - shootPoint.position).normalized;

			cannonHubTransform.localRotation = Quaternion.LookRotation(direction);
			cannonHubTransform.localEulerAngles = new Vector3(0f, cannonHubTransform.eulerAngles.y, 0f);
			
			cannonTransform.localRotation = Quaternion.LookRotation(direction);
			cannonTransform.localEulerAngles = new Vector3(cannonTransform.eulerAngles.x, 0f, 0f);
		}

		private Vector3 CalculatePrediction(int iterations)
		{
			Vector3 predictedPos = Target.transform.position;
    
			for (int i = 0; i < iterations; i++)
			{
				float distance = Vector3.Distance(shootPoint.position, predictedPos);
				float timeToTarget = distance / projectilePrefab.Speed;
				predictedPos = Target.transform.position + Target.Velocity * timeToTarget;
			}
    
			return predictedPos;
		}
	}
}

