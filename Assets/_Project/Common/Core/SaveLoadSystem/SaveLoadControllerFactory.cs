using UnityEngine;

namespace Project.Common.Core.SaveLoadSystem
{
    public class SaveLoadControllerFactory
    {
        private readonly GameObject _saveLoadControllerPrefab;

        private readonly SaveLoadModel _model;

        public SaveLoadControllerFactory(
            SaveLoadModel model, 
            GameObject saveLoadControllerPrefab)
        {
            _model = model;
            _saveLoadControllerPrefab = saveLoadControllerPrefab;
        }

        public SaveLoadController Create()
        {
            SaveLoadController saveLoadController;
            GameObject saveLoadControllerGameObject = GameObject.Instantiate(_saveLoadControllerPrefab, null);
            
            GameObject.DontDestroyOnLoad(saveLoadControllerGameObject);
            saveLoadController = saveLoadControllerGameObject.GetComponent<SaveLoadController>();
            saveLoadController.Initialize(_model);

            return saveLoadControllerGameObject.GetComponent<SaveLoadController>();
        }
    }
}