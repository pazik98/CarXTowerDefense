using System;
using UnityEngine;
using System.Collections;

namespace _CarXTowerDefense.Scripts.Tower
{
	public class CannonTower : Tower {
		
		[Header("Cannon")]
		[SerializeField] private float rotationSpeed = 1f;
		[SerializeField] private float maxVerticalRotationAngle = 89f;
		[SerializeField] private float minVerticalRotationAngle = -89f;
		[SerializeField] private float verticalAngleStep = 1f;
		[SerializeField] private float cannonAimTreshold = 0.1f;
		[SerializeField] private float verticalPredictionTreshold = 0.1f;
		[SerializeField] private CannonProjectile projectilePrefab;
		[SerializeField] private Transform cannonHubTransform;
		[SerializeField] private Transform cannonTransform;

		private Quaternion _targetRotation;
		
		protected override bool CanShoot => base.CanShoot && IsAimed;
		
		protected virtual bool IsAimed => Math.Abs(cannonTransform.eulerAngles.y - _targetRotation.eulerAngles.y) < cannonAimTreshold;

		protected override void Tick()
		{
			if (Target != null)
				RotateTower();
			
			base.Tick();
		}

		protected override void Shoot()
		{
			Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation); //TODO Pool
		}

		protected override void OnTargetDetected(Enemy enemy)
		{
			base.OnTargetDetected(enemy);
			_targetRotation = Quaternion.LookRotation((enemy.transform.position - shootPoint.position).normalized);
		}

		private void RotateTower()
		{
			var targetPosition = CalculateHorizontalPrediction(Target.transform.position, 3);
			var verticalAngleFounded = CalculateVerticalPredictionAngle(
				targetPosition, 
				Mathf.RoundToInt(Mathf.Max(Mathf.Abs(minVerticalRotationAngle), Mathf.Abs(maxVerticalRotationAngle)) / verticalAngleStep), 
				out var xAngle);

			_targetRotation = Quaternion.LookRotation((targetPosition - shootPoint.position).normalized);
			if (verticalAngleFounded)
			{
				_targetRotation.eulerAngles = new Vector3(-xAngle, _targetRotation.eulerAngles.y, 0);
			}
			
			var cannonHubActualRotation = Quaternion.Slerp(cannonHubTransform.localRotation, _targetRotation, rotationSpeed * Time.fixedDeltaTime);
			var cannonActualRotation = Quaternion.Slerp(cannonTransform.localRotation, _targetRotation, rotationSpeed * Time.fixedDeltaTime);
			
			cannonHubTransform.localRotation = cannonHubActualRotation;
			cannonHubTransform.localEulerAngles = new Vector3(0f, cannonHubTransform.eulerAngles.y, 0f);

			cannonTransform.localRotation = cannonActualRotation;
			cannonTransform.localEulerAngles = new Vector3(cannonTransform.eulerAngles.x, 0f, 0f);
		}

		private Vector3 CalculateHorizontalPrediction(Vector3 targetPosition, int iterations)
		{
			Vector3 predictedPos = targetPosition;
    
			for (int i = 0; i < iterations; i++)
			{
				float distance = Vector3.Distance(shootPoint.position, predictedPos);
				float timeToTarget = distance / projectilePrefab.Speed;
				predictedPos = Target.transform.position + Target.Velocity * timeToTarget;
			}
    
			return predictedPos;
		}

		private bool CalculateVerticalPredictionAngle(Vector3 targetPosition, int iterations, out float predictionAngle)
		{
			predictionAngle = 45f;
			Vector3 startPosition = shootPoint.position;
	        float distance = Vector3.Distance(startPosition, targetPosition);	
	        
	        // Start prediction
	        float angle = 0f;
	        float bestAngle = angle;
	        float bestError = float.MaxValue;
	        
	        for (int i = 0; i < iterations; i++)
	        {
	            float angleRad = angle * Mathf.Deg2Rad;
	            float horizontalSpeed = projectilePrefab.Speed * Mathf.Cos(angleRad);
	            float verticalSpeed = projectilePrefab.Speed * Mathf.Sin(angleRad);
	            
	            float timeToTarget = distance / horizontalSpeed;
	            
	            float predictedHeight = startPosition.y + verticalSpeed * timeToTarget - 0.5f * projectilePrefab.Gravity * timeToTarget * timeToTarget;
	            float error = Mathf.Abs(predictedHeight - targetPosition.y);
	            
	            if (error < bestError)
	            {
	                bestError = error;
	                bestAngle = angle;
	            }
	            
	            if (error < verticalPredictionTreshold)
	            {
	                predictionAngle = bestAngle;
	                return true;
	            }
	            
	            // Correction
	            if (predictedHeight < targetPosition.y)
	                angle += verticalAngleStep;
	            else
	                angle -= verticalAngleStep;
	                
	            if (angle > maxVerticalRotationAngle) angle = maxVerticalRotationAngle;
	            if (angle < minVerticalRotationAngle) angle = minVerticalRotationAngle;
	        }
	        
	        return false;
	    }
	}
}

