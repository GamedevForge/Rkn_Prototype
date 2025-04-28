using Project.Common.Core;
using Project.Common.Core.SaveLoadSystem;
using Project.Common.Installers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Project.Common.UI
{
    public class MainSceneLoader : MonoBehaviour
    {
        [SerializeField] private DayReceiver _dayReceiver;

        private ZenjectSceneLoader _sceneLoader;
        private DayHandler _dayHandler;
        private IProperty<PlayerSaveData> _saveData;
        private SaveLoadController _saveLoadController;

        [Inject] private void Construct(ZenjectSceneLoader sceneLoader,
            DayHandler dayHandler,
            IProperty<PlayerSaveData> saveData,
            SaveLoadController saveLoadController)
        {
            _sceneLoader = sceneLoader;
            _dayHandler = dayHandler;
            _saveData = saveData;
            _saveLoadController = saveLoadController;
        }

        public void LoadScene()
        {
            //_dayHandler.SetDayCount(_dayReceiver.DayNumber);
            _sceneLoader.LoadScene(_saveData.Property.SceneName, LoadSceneMode.Single, (container) =>
            {
                container.BindInstance(_dayReceiver.DayNumber).WhenInjectedInto<ProjectInstaller>();
            });
        }

        public void StartNewGame()
        {
            _saveLoadController.Clear();
            LoadScene();
        }
    }
}