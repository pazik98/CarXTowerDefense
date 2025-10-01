
using _CarXTowerDefense.Scripts.Tower;
using UnityEngine;

namespace _CarXTowerDefense.Scripts.Pool
{
    public interface IPoolable
    {
        void Spawn(Vector3 position, Quaternion rotation);
        void Despawn();
    }
}