using Project.Common.UI;
using System;
using Zenject;

namespace Project.Common.Configs
{
    public class QuestConfigService : IInitializable, IDisposable
    {
        private readonly QuestData _questData;
        private readonly DayHandler _dayHandler;

        public QuestConfig[] CurrentQuestConfigs { get; private set; }

        public QuestConfigService(QuestData questData, DayHandler dayHandler)
        {
            _questData = questData;
            _dayHandler = dayHandler;
        }

        public void Initialize()
        {
            _dayHandler.OnDayNumberChanged += ChangeCurrentQuestConfigs;
            ChangeCurrentQuestConfigs(_dayHandler.DayNumber);
        }

        public void Dispose() =>
            _dayHandler.OnDayNumberChanged -= ChangeCurrentQuestConfigs;

        private void ChangeCurrentQuestConfigs(int dayNumber)
        {
            if (dayNumber != 0)
                dayNumber--;
            if (dayNumber < _questData.QuestConfigs.Count)
                CurrentQuestConfigs = _questData.QuestConfigs[dayNumber];
            else
                CurrentQuestConfigs = null;
        }

        public QuestConfig GetQuestConfig()
        {
            if (CurrentQuestConfigs == null)
                return null;
            
            foreach (var config in CurrentQuestConfigs)
            {
                if (config.IsActive)
                    return config;
            }
            return null;
        }
    }
}
