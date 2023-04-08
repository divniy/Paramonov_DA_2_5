using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Pool<T> where T : MonoBehaviour
    {
        protected T _prefab;
        protected Transform _parent;
        private List<T> _elements;

        public Pool(T prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
            _elements = new();
        }

        protected void Init(int count)
        {
            for (int i = 0; i < count; i++)
                PoolUp(false);
        }

        public T Get(Vector3 spawnPosition)
        {
            T element = HasAvailable(out T availableElement) ? availableElement : PoolUp(true);
            element.transform.position = spawnPosition;
            return element;
        }

        public T Get(Vector3 spawnPosition, Quaternion spawnRotation)
        {
            T element = Get(spawnPosition);
            element.transform.rotation = spawnRotation;
            return element;
        }

        private bool HasAvailable(out T availableElement)
        {
            availableElement = _elements.Find(element => element.gameObject.activeSelf == false);
            availableElement?.gameObject.SetActive(true);
            return availableElement != default;
        }

        protected abstract T GetCreated();

        private T PoolUp(bool isActive)
        {
            T element = GetCreated();
            element.transform.SetParent(_parent);
            element.gameObject.SetActive(isActive);
            _elements.Add(element);
            return element;
        }
    }
}
