using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Project.Common.UI
{
    public class DataBaseViewController : IInitializable, IDisposable
    {
        private readonly GameObjectPool _pool;
        private readonly DataBaseWindowViewModel _model;
        private readonly Transform _parent;
        private readonly ResultSearchRepository _repository;

        public DataBaseViewController(
            GameObjectPool pool, 
            DataBaseWindowViewModel model, 
            Transform parent,
            ResultSearchRepository repository)
        {
            _pool = pool;
            _model = model;
            _parent = parent;
            _repository = repository;
        }
        
        public void Initialize() =>
            _model.OnFindResult += UpdateResults;

        public void Dispose() =>
            _model.OnFindResult -= UpdateResults;

        private void UpdateResults(Dictionary<string, string> results)
        {
            foreach (SearchButton searchButton in _repository.Objects)
            {
                searchButton.OnClick -= ChangeInfo;
                _pool.Release(searchButton.gameObject);
            }

            _repository.Clear();

            foreach(KeyValuePair<string, string> pair in results)
            {
                SearchButton searchButton = _pool.Get().GetComponent<SearchButton>();
                searchButton.Initialize(pair.Key, pair.Value);
                _repository.Add(searchButton);
                searchButton.OnClick += ChangeInfo;
                searchButton.transform.SetParent(_parent);
            }
        }

        private void ChangeInfo(string info) =>
            _model.ChangeTextInfo(info);
    }
}