using Project.Common.Core.SaveLoadSystem;
using System;
using UnityEngine;
using Zenject;

namespace Project.Common.Core
{
    public class SaveLoadPositionController : IInitializable, IDisposable
    {
        private readonly PlayerTransform _onDestroyEvent;
        private readonly ISaveController _saveController;
        private readonly IProperty<PlayerSaveData> _playerSaveData;

        public SaveLoadPositionController
            (PlayerComponents playerComponents,
            ISaveController saveController,
            IProperty<PlayerSaveData> playerSaveData)
        {
            _onDestroyEvent = playerComponents.OnDestroyEvent;
            _saveController = saveController;
            _playerSaveData = playerSaveData;
        }
        
        public void Initialize() =>
            _onDestroyEvent.PositionPerDestroyed += Save;

        public void Dispose() =>
            _onDestroyEvent.PositionPerDestroyed -= Save;

        private void Save(Vector3 lastPosition)
        {
            _playerSaveData.Property.PlayerWorldPosition = lastPosition;
            _saveController.Save();
        }
    }
}