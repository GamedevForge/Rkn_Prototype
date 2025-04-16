using Project.Common.Configs;
using System.Collections.Generic;
using Zenject;
using Project.Common.UI;
using Cysharp.Threading.Tasks;

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
        
        public void Initialize()
        {
            CurrentQuestConfig = _configService.GetQuestConfig();
            if (CurrentQuestConfig == null)
                return;

            if (CurrentQuestConfig.Type == QuestType.WithTarget)
                SetQuestView(GetQuestEvent(CurrentQuestConfig.ID).Target, CurrentQuestConfig).Forget();
            else
                SetQuestView(null, CurrentQuestConfig).Forget();
        }

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

        private async void CloseCurrentQuest(string id)
        {
            if (CurrentQuestConfig == null)
                return;
            
            //if (_questEvents.Count > 0)
            //{
                /*foreach (QuestEvent questEvent in _questEvents)
                {
                    if (id == questEvent.ID && id == CurrentQuestConfig.ID)
                    {
                        CurrentQuestConfig.IsActive = false;
                        await RemoveQuestView(CurrentQuestConfig);
                    }
                }*/
            //}
            //else
            //{
                if (id == CurrentQuestConfig.ID)
                {
                    CurrentQuestConfig.IsActive = false;
                    await RemoveQuestView(CurrentQuestConfig);
                }
            //}
            await GetNextQuest();
        }

        private async UniTask GetNextQuest()
        {
            CurrentQuestConfig = _configService.GetQuestConfig();
            if (CurrentQuestConfig.Type == QuestType.WithTarget)
                await SetQuestView(GetQuestEvent(CurrentQuestConfig.ID).Target, CurrentQuestConfig);
            else
                await SetQuestView(null, CurrentQuestConfig);
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

        private async UniTask SetQuestView(UnityEngine.Transform markerTarget, QuestConfig questConfig)
        {
            if (questConfig == null)
                return;
            
            await _view.ShowQuest(questConfig, markerTarget);
        }

        private UniTask RemoveQuestView(QuestConfig questConfig) =>
            _view.RemoveQuest(questConfig);
    }
}
