using Project.Common.Core.SaveLoadSystem;
using System;
using UnityEngine;

namespace Project.Common.Core
{
    public class PlayerWorldPosition : IPlyerWorldPositionProperty
    {
        public event Action OnTransformDisable;

        private readonly IProperty<PlayerSaveData> _saveLoadModel;
        
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

        public PlayerWorldPosition(IProperty<PlayerSaveData> saveLoadModel) =>
            _saveLoadModel = saveLoadModel;

        public void DisableCurrentTransform()
        {
            _saveLoadModel.Property.PlayerWorldPosition = PlayerPosition;
            _currentPlayerTransform = null;
            OnTransformDisable?.Invoke();
        }

        public void SetCurrentPlayerTransform(Transform currentPlayerTransform)
        {
            _currentPlayerTransform = currentPlayerTransform;
            _currentPlayerTransform.position = _saveLoadModel.Property.PlayerWorldPosition;
        }
    }
}