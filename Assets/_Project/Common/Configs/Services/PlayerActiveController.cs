using StarterAssets;
using System;
using UnityEngine;
using Zenject;

namespace Project.Common.Configs
{
    public class PlayerActiveController : IInitializable, IDisposable
    {
        private readonly GameObject _playerGameObject;
        private readonly GameState _gameState;

        private StarterAssetsInputs _gameInputs => _playerGameObject.GetComponent<StarterAssetsInputs>();

        public PlayerActiveController(
            GameObject playerGameObject,
            GameState gameState)
        {
            _playerGameObject = playerGameObject;
            _gameState = gameState;
        }
        
        public void Initialize()
        {
            DeactivatePlayerGameObject();
            _gameState.OnGameStart += ActivatePlayerGameObject;
            _gameState.OnGameStop += DeactivatePlayerGameObject;
        }

        public void Dispose()
        {
            _gameState.OnGameStart -= ActivatePlayerGameObject;
            _gameState.OnGameStop -= DeactivatePlayerGameObject;
        }

        private void ActivatePlayerGameObject()
        {
            _playerGameObject.SetActive(true);
            _gameInputs.HideCursor();         
        }

        private void DeactivatePlayerGameObject()
        {
            _gameInputs.ShowCursor();
            _playerGameObject.SetActive(false);
        }
    }
}