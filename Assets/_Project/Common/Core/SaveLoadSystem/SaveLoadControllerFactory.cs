using Project.Common.Configs;
using Project.Common.UI;
using UnityEngine;
using Zenject;

namespace Project.Common.Core.SaveLoadSystem
{
    public class SaveLoadControllerFactory : IInitializable
    {
        private readonly GameObject _saveLoadControllerPrefab;

        private readonly SaveLoadModel _model;
        private readonly IPlyerWorldPositionProperty _position;
        private readonly DayHandler _dayHandler;
        private readonly QuestConfigService _questConfigService;

        public SaveLoadControllerFactory(
            SaveLoadModel model, 
            IPlyerWorldPositionProperty position, 
            DayHandler dayHandler, 
            QuestConfigService questConfigService,
            GameObject saveLoadControllerPrefab)
        {
            _model = model;
            _position = position;
            _dayHandler = dayHandler;
            _questConfigService = questConfigService;
            _saveLoadControllerPrefab = saveLoadControllerPrefab;
        }

        public void Initialize() =>
            Create().Initialize(_model, _position, _dayHandler, _questConfigService);

        private SaveLoadController Create()
        {
            GameObject saveLoadControllerGameObject = GameObject.Instantiate(_saveLoadControllerPrefab, null);
            GameObject.DontDestroyOnLoad(saveLoadControllerGameObject);

            return saveLoadControllerGameObject.GetComponent<SaveLoadController>();
        }
    }
}