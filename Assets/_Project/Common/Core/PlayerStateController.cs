using UnityEngine;
using Zenject;
using System;
using StarterAssets;

namespace Project.Common.Core
{
    public class PlayerStateController : IInitializable, IDisposable
    {
        private readonly FirstPersonController _firstPersonController;
        private readonly CharacterController _characterController;
        private readonly ISitAndStandUpEvents _playerState;

        public PlayerStateController(FirstPersonController firstPersonController, 
            CharacterController characterController,
            ISitAndStandUpEvents playerState)
        {
            _firstPersonController = firstPersonController;
            _characterController = characterController;
            _playerState = playerState;
        }

        public void Initialize()
        {
            _playerState.OnSit += Sit;
            _playerState.OnLookedEnable += EnableLooked;
        }
        
        public void Dispose()
        {        
            _playerState.OnSit -= Sit;
            _playerState.OnLookedEnable -= EnableLooked;
        }

        private void Sit()
        {
            _firstPersonController.DisableMoving();
            _firstPersonController.DisableLooked();
            _characterController.enabled = false;
        }

        private void EnableLooked() =>
            _firstPersonController.EnableLooked();
    }
}


