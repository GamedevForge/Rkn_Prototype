using Project.Common.Configs;
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
}
