using System;
using Zenject;

namespace Project.Common.Configs
{
    public class GameState : IInitializable
    {
        public event Action OnMenu;
        public event Action OnPlayground;
        public event Action OnHome;
        public event Action OnGameStart;
        public event Action OnGameStop;

        public bool InMenuScene { get; private set; } = false;
        public bool InGameScene { get; private set; } = false;
        public bool InHomeScene { get; private set; } = false;
        public bool GameIsActive { get; private set; } = false;
        
        public void Initialize() =>
            InMenuScene = true;

        public void StartGame()
        {
            GameIsActive = true;
            OnGameStart?.Invoke();
        }
        
        public void QuitGame()
        {
            GameIsActive = false;
            OnGameStop?.Invoke();
        }

        public void GoToMenu()
        {
            InMenuScene = true;
            InGameScene = false;
            InHomeScene = false;
            OnMenu?.Invoke();
        }

        public void GoToPlayground()
        {
            InGameScene = true;
            InMenuScene = false;
            InHomeScene = false;
            OnPlayground?.Invoke();
        }

        public void GoToHome()
        {
            InHomeScene = true;
            InMenuScene = false;
            InGameScene = false;
            OnHome?.Invoke();
        }
    }
}