using UnityEngine;

namespace _CarXTowerDefense.Scripts.Pool
{
    public interface IObjectPool
    {
        IPoolable Get(Vector3 position, Quaternion rotation);
        
        void Return(IPoolable obj);
    }
}