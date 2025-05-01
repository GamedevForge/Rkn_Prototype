using Zenject;
using Project.Common.UI;
using System;
using Project.Common.Configs;

namespace Project.Common.Core
{
    public class GameQuitController : IInitializable, IDisposable
    {
        private readonly DayHandler _dayHandler;
        private readonly ZenjectSceneLoader _sceneLoader;
        private readonly GameState _gameState;

        public GameQuitController(
            DayHandler dayHandler, 
            ZenjectSceneLoader sceneLoader,
            GameState gameState)
        {
            _dayHandler = dayHandler;
            _sceneLoader = sceneLoader;
            _gameState = gameState;
        }
        
        public void Initialize() =>
            _dayHandler.OnDayNumberChanged += Quit;

        public void Dispose() =>
            _dayHandler.OnDayNumberChanged -= Quit;

        private void Quit()
        {
            if (_dayHandler.DayNumber >= 3)
            {
                _gameState.GoToMenu();
                _gameState.QuitGame();
                _sceneLoader.LoadScene("MainMenuScene");
            }
        }
    }
}


