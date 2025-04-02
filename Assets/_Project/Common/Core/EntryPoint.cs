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
        private readonly PlayerQuitController _playerQuitController;
        private readonly FirstPersonController _firstPersonController;
        private readonly CursorAnimation _cursorAnimation;

        public EntryPoint(PlayerState playerState,
            PlayerInteractController playerInteractController,
            PlayerRayCasterController rayCasterController,
            PlayerRayCasterModel rayCasterModel,
            FirstPersonController firstPersonController,
            CharacterController characterController,
            TextView interactiveObjectsTextView,
            CursorAnimation cursorAnimation,
            PlayerQuitController playerQuitController,
            NewsWindowData newsWindowData)
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
            
            _firstPersonController = firstPersonController;
            _playerQuitController = playerQuitController;
            _cursorAnimation = cursorAnimation;

            CreateNewsWindow(newsWindowData);
        }

        public void Initialize()
        {
            _interactController.Initialize(_rayCasterModel);
            _rayCasterController.Initialize(_rayCasterModel);
            _playerStateController.Initialize();
            _textController.Initialize();
            _playerQuitController.Initialize(_playerState, _firstPersonController);
            _cursorAnimation.Initialize();
        }

        public void Dispose()
        {
            _playerStateController.Dispose();
            _textController.Dispose();
            _cursorAnimation.Dispose();
        }

        private void CreateNewsWindow(NewsWindowData newsWindowData)
        {
            NewsWindowModelView newsWindowModel = new();
            WindowBaseAnimation windowBaseAnimation = new(
                newsWindowData.TargetRectTransform,
                newsWindowData.CanvasRectTransform,
                newsWindowData.NewsButtonTransform,
                newsWindowData.Duration);
            windowBaseAnimation.Initialize();
            NewsWindowController newsWindowController = new(windowBaseAnimation, newsWindowModel, newsWindowData.TargetRectTransform);
            newsWindowData.NewsWidget.Initialize(newsWindowController, newsWindowModel);
        }
    }
}


