using Project.Common.Configs;
using System.Collections.Generic;
using Zenject;
using Project.Common.UI;

namespace Project.Common.Core.Quest
{
    public class QuestController : IInitializable
    {
        private readonly QuestConfigService _configService;
        private readonly QuestViewController _view;
        private readonly List<QuestEvent> _questEvents = new();

        public QuestConfig CurrentQuestConfig { get; private set; }

        public QuestController(QuestConfigService configService, QuestViewController questView)
        {
            _configService = configService;
            _view = questView;
        }
        
        public void Initialize() =>
            CurrentQuestConfig = _configService.GetQuestConfig();

        public void AddQuestEvent(QuestEvent questEvent)
        {
            _questEvents.Add(questEvent);
            questEvent.OnEvent += CloseCurrentQuest;
        }

        public void RemoveQuestEvent(QuestEvent questEvent)
        {
            _questEvents.Remove(questEvent);
            questEvent.OnEvent -= CloseCurrentQuest;
        }

        private void CloseCurrentQuest(string id)
        {
            if (CurrentQuestConfig == null)
                return;
            
            foreach (QuestEvent questEvent in _questEvents)
            {
                if (id == questEvent.ID && id == CurrentQuestConfig.ID)
                {
                    CurrentQuestConfig.IsActive = false;
                    RemoveQuestView(CurrentQuestConfig);
                }
            }
            GetNextQuest();
        }

        private void GetNextQuest()
        {
            CurrentQuestConfig = _configService.GetQuestConfig();
            SetQuestView(GetQuestEvent(CurrentQuestConfig.ID).Target, CurrentQuestConfig);
        }

        private QuestEvent GetQuestEvent(string id)
        {
            foreach(QuestEvent questEvent in _questEvents)
            {
                if (id == questEvent.ID)
                    return questEvent;
            }
            return null;
        }

        private void SetQuestView(UnityEngine.Transform markerTarget, QuestConfig questConfig)
        {
            if (questConfig == null)
                return;
            
            _view.ShowQuest(questConfig, markerTarget);
        }

        private void RemoveQuestView(QuestConfig questConfig) =>
            _view.RemoveQuest(questConfig);
    }
}
