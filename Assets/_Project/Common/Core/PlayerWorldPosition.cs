using Project.Common.Core.SaveLoadSystem;
using System;
using UnityEngine;

namespace Project.Common.Core
{
    public class PlayerWorldPosition : IPlyerWorldPositionProperty
    {
        public event Action OnTransformDisable;

        private readonly IProperty<PlayerSaveData> _saveLoadModel;
        private readonly ISaveController _saveController;
        
        private Transform _currentPlayerTransform;

        public Vector3 PlayerPosition
        {
            get
            {
                if (_currentPlayerTransform != null)
                    return _currentPlayerTransform.position;
                else
                    return Vector3.zero;
            }
        }
        public bool CurrentPlayerTransformIsNotNull => _currentPlayerTransform != null;

        public PlayerWorldPosition(IProperty<PlayerSaveData> saveLoadModel, ISaveController saveController)
        {
            _saveLoadModel = saveLoadModel;
            _saveController = saveController;
        }

        public void SetPosition(Vector3 position) =>
            _saveLoadModel.Property.PlayerWorldPosition = position;

        public void DisableCurrentTransform()
        {
            _currentPlayerTransform = null;
            _saveController.Save();
            OnTransformDisable?.Invoke();
        }

        public void SetCurrentPlayerTransform(Transform currentPlayerTransform)
        {
            _currentPlayerTransform = currentPlayerTransform;
            _currentPlayerTransform.position = _saveLoadModel.Property.PlayerWorldPosition;
        }
    }
}