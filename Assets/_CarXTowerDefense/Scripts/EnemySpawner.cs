using System;
using UnityEngine;
using System.Collections;

namespace _CarXTowerDefense.Scripts
{
	public class EnemySpawner : MonoBehaviour
	{
		[SerializeField] Transform spawnPoint;
		[SerializeField] GameObject enemyPrefab;
		
		public float spawnInterval = 5f;
		private float _spawnTimer;

		private void FixedUpdate()
		{
			if (_spawnTimer <= 0)
			{
				Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
				_spawnTimer = spawnInterval;
			}
			else
			{
				_spawnTimer -= Time.fixedDeltaTime;
			}
		}
	}
}
