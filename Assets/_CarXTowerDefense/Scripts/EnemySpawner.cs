using System;
using UnityEngine;
using System.Collections;
using _CarXTowerDefense.Scripts.Pool;

namespace _CarXTowerDefense.Scripts
{
	public class EnemySpawner : MonoBehaviour
	{
		[SerializeField] private Transform spawnPoint;
		[SerializeField] private GameObject enemyPrefab;
		[SerializeField] private float spawnInterval = 5f;
		
		private float _spawnTimer;
		private EnemyPool _pool;

		private void Start()
		{
			_pool = PoolManager.Instance.EnemyPool;
		}

		private void FixedUpdate()
		{
			if (_spawnTimer <= 0)
			{
				_pool.Get(spawnPoint.position, spawnPoint.rotation);
				_spawnTimer = spawnInterval;
			}
			else
			{
				_spawnTimer -= Time.fixedDeltaTime;
			}
		}
	}
}
