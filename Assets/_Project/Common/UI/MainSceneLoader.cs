using Project.Common.Configs;
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
        private GameState _gameState;
        private PlayerPositionController _playerPositionController;

        [Inject] private void Construct(
            ZenjectSceneLoader sceneLoader,
            DayHandler dayHandler,
            IProperty<PlayerSaveData> saveData,
            SaveLoadController saveLoadController,
            GameState gameState,
            PlayerPositionController playerPositionController)
        {
            _sceneLoader = sceneLoader;
            _dayHandler = dayHandler;
            _saveData = saveData;
            _saveLoadController = saveLoadController;
            _gameState = gameState;
            _playerPositionController = playerPositionController;
        }

        private void LoadScene()
        {
            _gameState.StartGame();
            if (_saveData.Property.SceneName == "Playground")
                _gameState.GoToPlayground();
            else
                _gameState.GoToHome();

            _sceneLoader.LoadScene(_saveData.Property.SceneName, LoadSceneMode.Single, (container) =>
            {
                container.BindInstance(_dayReceiver.DayNumber).WhenInjectedInto<ProjectInstaller>();
            });
        }

        public void StartNewGame()
        {
            _saveLoadController.Clear();
            _dayHandler.SetDayCountOnStartGame();
            _playerPositionController.SetOriginPositionOnNeighborhood();
            LoadScene();
        }

        public void ContinueGame()
        {
            _playerPositionController.SetPosition(_saveData.Property.PlayerWorldPosition);
            LoadScene();
        }
    }
}