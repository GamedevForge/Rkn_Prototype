using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Project.Common.Core
{
    public class PlayerComponents : IInitializable
    {
        private readonly PlayerWorldPosition _playerWorldPosition;
        private readonly PlayerTransform _playerTransform;

        public readonly NavMeshAgent Agent;
        public readonly Transform CameraTransform;
        public readonly Transform PlayerTransform;
        
        public PlayerComponents(
            NavMeshAgent agent,
            Transform transform,
            Transform playerTransform,
            PlayerTransform playerTransformComponent, 
            PlayerWorldPosition playerWorldPosition)
        { 
            Agent = agent;
            CameraTransform = transform;
            PlayerTransform = playerTransform;
            _playerWorldPosition = playerWorldPosition;
            _playerTransform = playerTransformComponent;
        }

        public void Initialize()
        {
            _playerWorldPosition.SetCurrentPlayerTransform(PlayerTransform);
            _playerTransform.PositionPerDestroyed += OnDestroy;
        }

        private void OnDestroy(Vector3 lastPosition)
        {
            _playerWorldPosition.DisableCurrentTransform(lastPosition);
            _playerTransform.PositionPerDestroyed -= OnDestroy;
        }
    }
}


