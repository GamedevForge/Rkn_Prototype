using Project.Common.Configs;
using Project.Common.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Common.Core.SaveLoadSystem
{
    public class SaveLoadController : MonoBehaviour
    {
        private readonly SaveLoadSystem _system = new();

        private SaveLoadModel _model;
        private IPlyerWorldPositionProperty _position;
        private DayHandler _dayHandler;
        private QuestConfigService _questConfigService;

        public void Initialize(
            SaveLoadModel model, 
            IPlyerWorldPositionProperty position, 
            DayHandler dayHandler,
            QuestConfigService questConfigService)
        {
            _model = model;
            _position = position;
            _dayHandler = dayHandler;
            _questConfigService = questConfigService;

            PlayerSaveData playerSaveData = _system.Load();

            if (playerSaveData == null)
            {
                playerSaveData = new()
                {
                    PlayerWorldPosition = _position.PlayerPosition,
                    Day = _dayHandler.DayNumber,
                    SceneName = "Neighborhood",
                };
            }

            _model.SetCurrentData(playerSaveData);

            _dayHandler.OnDayNumberChanged += Save;
            _questConfigService.OnQuestClose += Save;
            _position.OnTransformDisable += Save;
        }

        private void OnDestroy()
        {
            _dayHandler.OnDayNumberChanged -= Save;
            _questConfigService.OnQuestClose -= Save;
            _position.OnTransformDisable -= Save;

            Save();
        }

        public void Save()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;

            if (currentSceneName != "MainMenuScene")
                _model.Property.SceneName = currentSceneName;

            _system.Save(_model.Property);
        }

        public void Save(int _) =>
            Save();
    }
}