using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Project.Common.Core
{
    public class PlayerComponents : IInitializable, IDisposable
    {
        private readonly PlayerWorldPosition _playerWorldPosition;
        
        public readonly NavMeshAgent Agent;
        public readonly Transform CameraTransform;
        public readonly Transform PlayerTransform;

        public PlayerComponents(
            NavMeshAgent agent,
            Transform transform,
            Transform playerTransform,
            PlayerWorldPosition playerWorldPosition) 
        { 
            Agent = agent;
            CameraTransform = transform;
            PlayerTransform = playerTransform;
            _playerWorldPosition = playerWorldPosition;
        }

        public void Initialize() =>
            _playerWorldPosition.SetCurrentPlayerTransform(CameraTransform);

        public void Dispose() =>
            _playerWorldPosition.DisableCurrentTransform();
    }
}


