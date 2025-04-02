using Project.Common.Configs;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Project.Common.UI
{
    public class NewsModel
    {
        private readonly NewsListData _data;

        public NewsConfig CurrentNews { get; private set; }

        public NewsModel(NewsListData data)
        {
            _data = data;
        }

        public void ChangeCurrentNews() =>
            CurrentNews = _data.NewsConfigs[Random.Range(0, _data.NewsConfigs.Length)];
    }

    public class DataBaseModel
    {
        private readonly InquiriesData _data;

        public IEnumerable<KeyValuePair<string, string>> Inquiries => _data.Inquiries;

        public DataBaseModel(InquiriesData data)
        {
            _data = data;
        }
    }

    public class DataBaseController : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;

        private DataBaseModel _model;

        public void Initialize(DataBaseModel model)
        {
            _model = model;
        }


    }
}
