using ModestTree;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Common.UI
{
    public class DataBaseViewController : IInitializable, IDisposable
    {
        private readonly GameObjectPool _pool;
        private readonly DataBaseWindowViewModel _model;
        private readonly Transform _parent;
        private readonly Transform _secondParent;
        private readonly ResultSearchRepository _repository;

        private GridLayoutGroup GridLayoutGroup => _parent.GetComponent<GridLayoutGroup>();

        public DataBaseViewController(
            GameObjectPool pool, 
            DataBaseWindowViewModel model, 
            Transform parent,
            ResultSearchRepository repository,
            Transform secondParent)
        {
            _pool = pool;
            _model = model;
            _parent = parent;
            _repository = repository;
            _secondParent = secondParent;
        }
        
        public void Initialize() =>
            _model.OnFindResult += UpdateResults;

        public void Dispose()
        {
            _model.OnFindResult -= UpdateResults;

            foreach (SearchButton searchButton in _repository.Objects)
                searchButton.OnClick -= ChangeInfo;
        }

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
                searchButton.transform.localScale = Vector3.one;
                searchButton.transform.localPosition = new Vector3(
                    searchButton.transform.localPosition.x, 
                    searchButton.transform.localPosition.y, 
                    0f);
                searchButton.transform.SetParent(_parent);
            }
        }

        private void ChangeInfo(string info) =>
            _model.ChangeTextInfo(info);
    }
}