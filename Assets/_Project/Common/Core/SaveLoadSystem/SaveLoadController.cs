using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Common.Core.SaveLoadSystem
{
    public class SaveLoadController : MonoBehaviour, ISaveController
    {
        private readonly SaveLoadSystem _system = new();

        private SaveLoadModel _model;
        private PlayerPositionController _positionController;

        public void Initialize(
            SaveLoadModel model,
            PlayerPositionController playerPositionController)
        {
            _model = model;
            _positionController = playerPositionController;

            PlayerSaveData playerSaveData = _system.Load();

            if (playerSaveData == null)
            {
                playerSaveData = new()
                {
                    PlayerWorldPosition = Vector3.zero,
                    Day = 1,
                    SceneName = "Neighborhood",
                };
            }

            _model.SetCurrentData(playerSaveData);
        }

        private void OnDestroy()
        {
            Save();
        }

        public void Save()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            _model.Property.PlayerWorldPosition = _positionController.CurrentPosition;

            if (currentSceneName != "MainMenuScene")
                _model.Property.SceneName = currentSceneName;

            _system.Save(_model.Property);
        }

        public void Clear()
        {
            PlayerSaveData playerSaveData = new()
            {
                PlayerWorldPosition = Vector3.zero,
                Day = 1,
                SceneName = "Neighborhood",
                QuestSaveData = null,
            };
            _model.SetCurrentData(playerSaveData);
        }

        public void Save(int _) =>
            Save();
    }
}