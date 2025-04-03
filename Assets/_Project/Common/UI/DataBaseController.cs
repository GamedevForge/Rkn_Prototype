using TMPro;
using UnityEngine;

namespace Project.Common.UI
{
    public class DataBaseController : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;

        private DataBaseModel _model;
        private SearchEngineBase _engine;

        public void Initialize(DataBaseModel model, SearchEngineBase searchEngineBase)
        {
            _model = model;
            _engine = searchEngineBase;

            _model.OnRequestChanged += FindResults;
        }

        private void OnDestroy() =>
            _model.OnRequestChanged -= FindResults;

        public void FindResults(string request) =>
            _model.ChangeFindResults(_engine.Search(request));

        public void OnInput() =>
            _model.ChangeValue(_inputField.text);
    }
}
