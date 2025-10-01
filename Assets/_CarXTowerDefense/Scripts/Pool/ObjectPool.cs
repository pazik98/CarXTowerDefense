using System;
using System.Collections.Generic;
using UnityEngine;

namespace _CarXTowerDefense.Scripts.Pool
{
    public class ObjectPool<T> : MonoBehaviour, IObjectPool where T : MonoBehaviour, IPoolable
    {
        [SerializeField] private T prefab;
        [SerializeField] private int initSize = 10;

        private Queue<T> _queue;

        private void Start()
        {
            _queue = new Queue<T>(initSize);
            for (int i = 0; i < initSize; i++)
            {
                _queue.Enqueue(Create());
            }
        }

        private T Create()
        {
            var obj = Instantiate(prefab);
            obj.gameObject.SetActive(false);
            return obj;
        }
        
        private T GetObject(Vector3 position, Quaternion rotation)
        {
            var obj = _queue.Count > 0 ? _queue.Dequeue() : Create();
            obj.Spawn(position, rotation);
            return obj;
        }

        private void ReturnObject(T obj)
        {
            obj.gameObject.SetActive(false);
            _queue.Enqueue(obj);
        }

        public IPoolable Get(Vector3 position, Quaternion rotation)
        {
            return GetObject(position, rotation);
        }
        
        public void Return(IPoolable obj)
        {
            ReturnObject(obj as T);
        }
    }
}