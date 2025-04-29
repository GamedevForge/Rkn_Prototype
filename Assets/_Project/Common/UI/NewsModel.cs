using Project.Common.Configs;
using System;
using Zenject;

namespace Project.Common.UI
{
    public class NewsModel : IInitializable, IDisposable
    {
        private readonly NewsListData _data;
        private readonly DayHandler _dayHandler;

        private int _newsCountPerDay = 0;

        public NewsConfig CurrentNews { get; private set; }
        public bool NewsIsNotOverForToday => _newsCountPerDay < _data.NewsPerDay;

        public NewsModel(NewsListData data, DayHandler dayHandler)
        {
            _data = data;
            _dayHandler = dayHandler;

            ChangeCurrentNews();
        }
        
        public void Initialize() =>
            _dayHandler.OnDayNumberChanged += ZeroOut;

        public void Dispose() =>
            _dayHandler.OnDayNumberChanged -= ZeroOut;

        public void ChangeCurrentNews() =>
            CurrentNews = _data.NewsConfigs[UnityEngine.Random.Range(0, _data.NewsConfigs.Length)];

        public void ApproveOrRejectNews()
        {
            if (NewsIsNotOverForToday)
                _newsCountPerDay++;
        }

        private void ZeroOut() =>
            _newsCountPerDay = 0;
    }
}
