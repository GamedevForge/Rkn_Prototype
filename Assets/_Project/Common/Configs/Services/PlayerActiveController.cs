using System;
using UnityEngine;
using Zenject;

namespace Project.Common.Configs
{
    public class PlayerActiveController : IInitializable, IDisposable
    {
        private readonly GameObject _playerGameObject;
        private readonly GameState _gameState;

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

        private void ActivatePlayerGameObject() =>
            _playerGameObject.SetActive(true);

        private void DeactivatePlayerGameObject() =>
            _playerGameObject.SetActive(false);
    }
}