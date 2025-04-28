using Project.Common.Configs;
using System.Collections.Generic;
using Zenject;
using Project.Common.UI;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using System;

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
        
        public async void Initialize()
        {
            CurrentQuestConfig = _configService.GetQuestConfig();
            if (CurrentQuestConfig == null)
                return;

            await UniTask.WaitWhile(() => CurrentQuestConfig.SceneName != SceneManager.GetActiveScene().name);

            if (CurrentQuestConfig.Type == QuestType.WithTarget)
            {
                _view.EnableTargetPointer();
                SetQuestView(GetQuestEvent(CurrentQuestConfig.ID).Target, CurrentQuestConfig).Forget();
            }
            else
            {
                _view.DisableTargetPointer();
                SetQuestView(null, CurrentQuestConfig).Forget();
            }
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
            _view.DisableTargetPointer();
            if (CurrentQuestConfig == null)
                return;

            if (id == CurrentQuestConfig.ID)
            {
                //CurrentQuestConfig.IsActive = false;
                _configService.CloseQuest(CurrentQuestConfig.ID);
                await RemoveQuestView(CurrentQuestConfig);
                await GetNextQuest();
            }
        }

        private async UniTask GetNextQuest()
        {
            CurrentQuestConfig = _configService.GetQuestConfig();

            if (CurrentQuestConfig == null)
                return;

            await UniTask.WaitWhile(() => CurrentQuestConfig.SceneName != SceneManager.GetActiveScene().name);

            if (CurrentQuestConfig.Type == QuestType.WithTarget)
            {
                _view.EnableTargetPointer();
                await SetQuestView(GetQuestEvent(CurrentQuestConfig.ID).Target, CurrentQuestConfig);
            }
            else
            {
                _view.DisableTargetPointer();
                await SetQuestView(null, CurrentQuestConfig);
            }
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