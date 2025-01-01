using System.Collections.Generic;
using Interface;
using UnityEngine;

namespace New
{
    public class ObjectPool<T> where T : MonoBehaviour, IPoolable
    {
        private List<T> allObjects = new List<T>();
        public IEnumerable<T> Objects => allObjects; // Доступ ко всем объектам

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
                allObjects.Add(obj); // Добавляем в список всех объектов
            }
        }

        public T Get()
        {
            if (pool.Count > 0)
            {
                T obj = pool.Dequeue();
                obj.gameObject.SetActive(true);
                obj.OnSpawn();
                return obj;
            }

            T newObj = GameObject.Instantiate(prefab, parent);
            allObjects.Add(newObj); // Добавляем новый объект в список всех
            newObj.OnSpawn();
            return newObj;
        }

        public void Return(T obj)
        {
            if (!allObjects.Contains(obj))
            {
                Debug.LogError($"ObjectPool<{typeof(T)}> - Попытка вернуть объект, который не принадлежит пулу!");
                return;
            }

            obj.OnDespawn();
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}