using System;
using UnityEngine;

namespace Project.Common.Core
{
    public class PlayerTransform : MonoBehaviour
    {
        public event Action<Vector3> PositionPerDestroyed;

        [SerializeField] private Transform _playerTransform;

        private void OnDestroy() =>
            PositionPerDestroyed?.Invoke(_playerTransform.position);
    }
}