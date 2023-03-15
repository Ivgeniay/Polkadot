using System.Collections.Generic;
using UnityEngine;

namespace Architecture.Pools.ObjectsPool
{
    public class MonoPool<T> where T : MonoBehaviour
    {
        private T instance { get; }
        public bool isAutoExpandable { get; set; } = true;
        private ObjectPool<T> pool;
        private Transform container;

        public MonoPool(T instance, int count = 0, Transform container = null, bool isAutoExpandable = true)
        {
            this.instance = instance;
            if (container == null) {
                var go = new GameObject($"PoolOf:{instance}");
                this.container = go.transform;
            }
            else  { 
                this.container = container; 
            }
            this.isAutoExpandable = isAutoExpandable;

            pool = CreatePool(count, this.container);
        }

        private ObjectPool<T> CreatePool(int count = 0, Transform container = null)
        {
            pool = new ObjectPool<T>();

            for (int i = 0; i < count; i++)
            {
                CreateObject(container);
            }

            return pool;
        }

        public T CreateObject(Transform container = null, bool isActiveOnInstantiate = false)
        {
            if (container == null)
                container = this.container;

            var go = Object.Instantiate(instance, container);
            go.gameObject.SetActive(isActiveOnInstantiate);
            pool.AddObject(go);
            return go;
        }

        public bool HasFreeElement(out T _object)
        {
            foreach (var go in pool)
            {
                if (!go.gameObject.activeSelf)
                {
                    _object = go;
                    go.gameObject.SetActive(true);
                    return true;
                }
            }
            _object = null;
            return false;
        }

        public T GetUnactiveObject()
        {
            if (HasFreeElement(out var _object))
            {
                return _object;
            }

            if (isAutoExpandable)
                return CreateObject();

            return null;
        }
        public List<T> GetAllUnactiveObject()
        {
            List<T> list = new List<T>();

            foreach (var go in pool)
            {
                if (!go.gameObject.activeSelf) list.Add(go);
            }

            return list;
        }

        public List<T> GetAllActiveObjects()
        {
            List<T> list = new List<T>();

            foreach (var go in pool)
            {
                if (go.gameObject.activeSelf) list.Add(go);
            }

            return list;
        }

        public int GetCountActiveObjects()
        {
            int counter = 0;
            foreach (var go in pool) { if (go.gameObject.activeSelf) counter++; }
            return counter;
        }
        public int GetCountUnactiveObjects()
        {
            int counter = 0;
            foreach (var go in pool) { if (!go.gameObject.activeSelf) counter++; }
            return counter;
        }
    }
}