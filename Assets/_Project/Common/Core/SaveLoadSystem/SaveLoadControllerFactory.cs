using UnityEngine;

namespace Project.Common.Core.SaveLoadSystem
{
    public class SaveLoadControllerFactory
    {
        private readonly GameObject _saveLoadControllerPrefab;
        private readonly SaveLoadModel _model;
        private readonly PlayerPositionController _positionController;

        public SaveLoadControllerFactory(
            SaveLoadModel model, 
            GameObject saveLoadControllerPrefab,
            PlayerPositionController playerPositionController)
        {
            _model = model;
            _saveLoadControllerPrefab = saveLoadControllerPrefab;
            _positionController = playerPositionController;
        }

        public SaveLoadController Create()
        {
            SaveLoadController saveLoadController;
            GameObject saveLoadControllerGameObject = GameObject.Instantiate(_saveLoadControllerPrefab, null);
            
            GameObject.DontDestroyOnLoad(saveLoadControllerGameObject);
            saveLoadController = saveLoadControllerGameObject.GetComponent<SaveLoadController>();
            saveLoadController.Initialize(_model, _positionController);

            return saveLoadControllerGameObject.GetComponent<SaveLoadController>();
        }
    }
}