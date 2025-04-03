using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Project.Common.UI
{
    public class GameObjectPool
    {
        private readonly ObjectPool<GameObject> _pool;
        private readonly IInstantiator _instantiator;
        private readonly int _poolMaxSize = 40;

        private GameObject _gameObject;
        
        public GameObjectPool(IInstantiator instantiator, GameObject gameObject)
        {
            _instantiator = instantiator;
            _gameObject = gameObject;

            _pool = new ObjectPool<GameObject>(OnCreatePrefab, OnGetPrefab, OnRelease, OnDestroyPrefab, false, _poolMaxSize);
        }

        private GameObject OnCreatePrefab() =>
            _instantiator.InstantiatePrefab(_gameObject);

        private void OnDestroyPrefab(GameObject obj) =>
            GameObject.Destroy(obj);

        private void OnRelease(GameObject obj) =>
            obj.SetActive(false);

        private void OnGetPrefab(GameObject obj) =>
            obj.SetActive(true);

        public GameObject Get() =>
            _pool.Get();

        public void Release(GameObject gameObject) =>
            _pool.Release(gameObject);
    }
}