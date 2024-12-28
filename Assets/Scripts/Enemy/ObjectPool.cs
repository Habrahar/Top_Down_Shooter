using System.Collections.Generic;
using Interface;
using UnityEngine;

namespace New
{
    public class ObjectPool<T> where T : MonoBehaviour, IPoolable
    {
        private Queue<T> pool = new Queue<T>();
        private T prefab;
        private Transform parent;

        public ObjectPool(T prefab, int initialSize, Transform parent = null)
        {
            this.prefab = prefab;
            this.parent = parent;

            for (int i = 0; i < initialSize; i++)
            {
                T obj = GameObject.Instantiate(prefab, parent);
                obj.gameObject.SetActive(false);
                pool.Enqueue(obj);
            }
        }

        public T Get()
        {
            if (pool.Count > 0)
            {
                T obj = pool.Dequeue();
                obj.gameObject.SetActive(true);
                obj.OnSpawn(); // Вызываем OnSpawn
                return obj;
            }

            T newObj = GameObject.Instantiate(prefab, parent);
            newObj.OnSpawn(); // Вызываем OnSpawn
            return newObj;
        }

        public void Return(T obj)
        {
            obj.OnDespawn(); // Вызываем OnDespawn
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }

}