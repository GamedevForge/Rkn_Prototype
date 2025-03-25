using UnityEngine;
using Zenject;
using System;
using StarterAssets;
using Project.Common.UI;

namespace Project.Common.Core
{
    public class EntryPoint : IInitializable, IDisposable
    {
        private readonly PlayerState _playerState;
        private readonly PlayerInteractController _interactController;
        private readonly PlayerRayCasterController _rayCasterController;
        private readonly PlayerRayCasterModel _rayCasterModel;
        private readonly PlayerStateController _playerStateController;
        private readonly InteractiveObjectsTextController _textController;

        public EntryPoint(PlayerState playerState,
            PlayerInteractController playerInteractController,
            PlayerRayCasterController rayCasterController,
            PlayerRayCasterModel rayCasterModel,
            FirstPersonController firstPersonController,
            CharacterController characterController,
            TextView interactiveObjectsTextView)
        {
            _playerState = playerState;
            _interactController = playerInteractController;
            _rayCasterController = rayCasterController;
            _rayCasterModel = rayCasterModel;
            _playerStateController = new(
                firstPersonController, 
                characterController, 
                _playerState);
            _textController = new(interactiveObjectsTextView,
                rayCasterModel);
        }

        public void Initialize()
        {
            _interactController.Initialize(_rayCasterModel);
            _rayCasterController.Initialize(_rayCasterModel);
            _playerStateController.Initialize();
            _textController.Initialize();
        }

        public void Dispose()
        {
            _playerStateController.Dispose();
            _textController.Dispose();
        }
    }
}


