using Project.Common.Core;
using Project.Common.Core.SaveLoadSystem;
using Project.Common.UI;
using System;
using System.Collections.Generic;
using Zenject;

namespace Project.Common.Configs
{
    public class QuestConfigService : IInitializable, IDisposable
    {
        public event Action OnQuestClose;
        public event Action OnCurrentQuestConfigChange;
        
        private readonly QuestData _questData;
        private readonly DayHandler _dayHandler;
        private readonly IProperty<PlayerSaveData> _playerSaveData;
        private readonly ISaveController _saveController;
        private readonly GameState _gameState;

        public QuestConfig[] CurrentQuestConfigs { get; private set; }

        public QuestConfigService(
            QuestData questData, 
            DayHandler dayHandler, 
            IProperty<PlayerSaveData> playerSaveData,
            ISaveController saveController,
            GameState gameState)
        {
            _questData = questData;
            _dayHandler = dayHandler;
            _playerSaveData = playerSaveData;
            _saveController = saveController;
            _gameState = gameState;
        }

        public void Initialize()
        {
            _dayHandler.OnDayNumberChanged += ChangeCurrentQuestConfigs;
            _gameState.OnGameStart += ChangeCurrentQuestConfigs;
        }

        public void Dispose()
        {
            _dayHandler.OnDayNumberChanged -= ChangeCurrentQuestConfigs;
            _gameState.OnGameStart -= ChangeCurrentQuestConfigs;
        }

        private void ChangeCurrentQuestConfigs()
        {
            int dayNumber = _dayHandler.DayNumber;
            if (_playerSaveData.Property.QuestSaveData == null)
            {       
                if (dayNumber != 0)
                    dayNumber--;
                if (dayNumber < _questData.QuestConfigs.Count)
                {
                    _playerSaveData.Property.QuestSaveData = new QuestSaveData();
                    _playerSaveData.Property.QuestSaveData.QuestSaveConfigs = new List<QuestSaveConfig>();
                    QuestSaveData questSaveData = _playerSaveData.Property.QuestSaveData;

                    CurrentQuestConfigs = _questData.QuestConfigs[dayNumber];
                    foreach (var config in CurrentQuestConfigs)
                        questSaveData.QuestSaveConfigs.Add(new QuestSaveConfig() { ID = config.ID, IsActive = true } );
                }
                else
                    CurrentQuestConfigs = null;
            }
            else
            {
                if (dayNumber != 0)
                    dayNumber--;
                if (dayNumber < _questData.QuestConfigs.Count)
                    CurrentQuestConfigs = _questData.QuestConfigs[dayNumber];
                else
                {
                    CurrentQuestConfigs = null;
                    _playerSaveData.Property.QuestSaveData = null;
                }
            }
            OnCurrentQuestConfigChange?.Invoke();
        }

        private QuestSaveConfig GetSaveDataConfig(string id)
        {
            foreach (var config in _playerSaveData.Property.QuestSaveData.QuestSaveConfigs)
            {
                if (config.ID == id)
                    return config;
            }
            return null;
        }

        public void CloseQuest(string id)
        {
            foreach (var config in _playerSaveData.Property.QuestSaveData.QuestSaveConfigs)
            {
                if (config.ID == id)
                {
                    config.IsActive = false;
                    break;
                }
            }
            _saveController.Save();
            OnQuestClose?.Invoke();
        }

        public QuestConfig GetQuestConfig()
        {
            if (CurrentQuestConfigs == null)
                return null;
            
            foreach (var config in CurrentQuestConfigs)
            {
                if (GetSaveDataConfig(config.ID).IsActive)
                    return config;
            }
            return null;
        }
    }
}