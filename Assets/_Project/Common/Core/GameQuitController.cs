using Zenject;
using Project.Common.UI;
using System;

namespace Project.Common.Core
{
    public class GameQuitController : IInitializable, IDisposable
    {
        private readonly DayHandler _dayHandler;
        private readonly ZenjectSceneLoader _sceneLoader;

        public GameQuitController(DayHandler dayHandler, ZenjectSceneLoader sceneLoader)
        {
            _dayHandler = dayHandler;
            _sceneLoader = sceneLoader;
        }
        
        public void Initialize()
        {
            _dayHandler.OnDayNumberChanged += Quit;
        }

        public void Dispose()
        {
            _dayHandler.OnDayNumberChanged -= Quit;
        }

        private void Quit()
        {
            if (_dayHandler.DayNumber >= 3)
                _sceneLoader.LoadScene("MainMenuScene");
        }
    }
}


