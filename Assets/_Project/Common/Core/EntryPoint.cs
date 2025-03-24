using UnityEngine;
using Zenject;
using System;
using StarterAssets;

namespace Project.Common.Core
{
    public class EntryPoint : IInitializable, IDisposable
    {
        private readonly PlayerState _playerState;
        private readonly PlayerInteractController _interactController;
        private readonly PlayerRayCasterController _rayCasterController;
        private readonly PlayerRayCasterModel _rayCasterModel;
        private readonly PlayerStateController _playerStateController;

        public EntryPoint(PlayerState playerState,
            PlayerInteractController playerInteractController,
            PlayerRayCasterController rayCasterController,
            PlayerRayCasterModel rayCasterModel,
            FirstPersonController firstPersonController,
            CharacterController characterController)
        {
            _playerState = playerState;
            _interactController = playerInteractController;
            _rayCasterController = rayCasterController;
            _rayCasterModel = rayCasterModel;
            _playerStateController = new(
                firstPersonController, 
                characterController, 
                _playerState);
        }

        public void Initialize()
        {
            _interactController.Initialize(_rayCasterModel);
            _rayCasterController.Initialize(_rayCasterModel);
            _playerStateController.Initialize();
        }

        public void Dispose()
        {
            _playerStateController.Dispose();
        }
    }
}


