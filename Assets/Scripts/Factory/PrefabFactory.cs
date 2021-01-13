using UnityEngine;

namespace Scripts
{
    public class PrefabFactory<T> : IFactory<T>
    {
        private GameObject _prefab;
        private bool _isActive;

        public PrefabFactory(GameObject prefab, bool isActive = true)
        {
            _prefab = prefab;
            _isActive = isActive;
        }

        public T Create()
        {
            var tempGameObject = Object.Instantiate(_prefab);
            var objectOfType = tempGameObject.GetComponent<T>();
            
            tempGameObject.SetActive(_isActive);

            return objectOfType;
        }
    }
}