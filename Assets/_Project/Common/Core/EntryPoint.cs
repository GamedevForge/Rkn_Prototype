using UnityEngine;
using Zenject;
using System;
using StarterAssets;
using Project.Common.UI;
using Project.Common.Installers;

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
        private readonly PlayerComponents _playerComponents;
        private readonly WindowsRepository _windowsRepository;
        private readonly IInstantiator _instantiator;

        private DataBaseWindowViewModel _dataBaseWindowViewModel;
        private DataBaseViewController _dataBaseViewController;

        public EntryPoint(PlayerState playerState,
            PlayerInteractController playerInteractController,
            PlayerRayCasterController rayCasterController,
            PlayerRayCasterModel rayCasterModel,
            FirstPersonController firstPersonController,
            CharacterController characterController,
            TextView interactiveObjectsTextView,
            IInstantiator instantiator,
            WindowsRepository windowsRepository,
            PlayerComponents playerComponents,
            CursorAnimation cursorAnimation,
            PlayerQuitController playerQuitController,
            NewsWindowData newsWindowData,
            DataBaseWindowData dataBaseWindowData,
            RequirementsWindowData requirementsWindowData)
        {
            _playerState = playerState;
            _interactController = playerInteractController;
            _rayCasterController = rayCasterController;
            _rayCasterModel = rayCasterModel;
            _playerComponents = playerComponents;
            _instantiator = instantiator;
            
            _playerStateController = new(
                firstPersonController, 
                characterController, 
                _playerState);
            _textController = new(interactiveObjectsTextView,
                rayCasterModel);
            
            _firstPersonController = firstPersonController;
            _playerQuitController = playerQuitController;
            _cursorAnimation = cursorAnimation;
            _windowsRepository = windowsRepository;

            CreateNewsWindow(newsWindowData);
            CreateDataBaseWindow(dataBaseWindowData);
            CreateRequirementsWindow(requirementsWindowData);
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
            _dataBaseWindowViewModel.Dispose();
            _dataBaseWindowViewModel.Dispose();
        }

        private void CreateNewsWindow(NewsWindowData newsWindowData)
        {
            NewsWindowViewModel newsWindowViewModel = new(newsWindowData.NewsImage);            
            WindowBaseAnimation windowBaseAnimation = new(
                newsWindowData.TargetRectTransform,
                newsWindowData.CanvasRectTransform,
                newsWindowData.NewsButtonTransform,
                newsWindowData.Duration);
            
            windowBaseAnimation.Initialize();
            BaseWindowController newsWindowController = new(windowBaseAnimation, newsWindowViewModel, newsWindowData.TargetRectTransform);
            newsWindowData.NewsWidget.Initialize(newsWindowController, newsWindowViewModel);
            newsWindowData.NewsCloseWidget.Initialize(newsWindowController, newsWindowViewModel);

            NewsModel newsModel = new(newsWindowData.News);
            NewsApproveOrRejectAnimations animation = new(
                newsWindowData.ArmTransform,
                _playerComponents,
                newsWindowData.CameraLookAtPointTransform,
                newsWindowData.ApproveOrRejectData,
                newsWindowData.ApproveButtonTransform,
                newsWindowData.RejectButtonTransform,
                _firstPersonController,
                _playerState);
            newsWindowData.NewsController.Initialize(newsModel, newsWindowViewModel, animation, _playerState);
            _windowsRepository.Add(newsWindowViewModel, newsWindowController);
        }

        private void CreateDataBaseWindow(DataBaseWindowData dataBaseWindowData)
        {
            DataBaseModel dataBaseModel = new(dataBaseWindowData.InquiriesData);
            _dataBaseWindowViewModel = new(dataBaseModel, dataBaseWindowData.TextInfo);
            GameObjectPool gameObjectPool = new(_instantiator, dataBaseWindowData.ButtonPrefab);
            ResultSearchRepository repository = new();
            SearchEngineBase searchEngine = new(dataBaseWindowData.InquiriesData.Inquiries);
            _dataBaseViewController = new(
                gameObjectPool, 
                _dataBaseWindowViewModel, 
                dataBaseWindowData.ButtonPrefabsParent, 
                repository);
            WindowBaseAnimation windowBaseAnimation = new(
                dataBaseWindowData.TargetRectTransform,
                dataBaseWindowData.CanvasRectTransform,
                dataBaseWindowData.DataBaseButtonTransform,
                dataBaseWindowData.Duration);
            windowBaseAnimation.Initialize();
            _dataBaseWindowViewModel.Initialize();
            _dataBaseViewController.Initialize();
            dataBaseWindowData.DataBaseController.Initialize(dataBaseModel, searchEngine);

            BaseWindowController dataBaseWindowController = new(
                windowBaseAnimation,
                _dataBaseWindowViewModel,
                dataBaseWindowData.TargetRectTransform);

            dataBaseWindowData.DataBaseWidget.Initialize(dataBaseWindowController, _dataBaseWindowViewModel);
            dataBaseWindowData.DataBaseCloseWidget.Initialize(dataBaseWindowController, _dataBaseWindowViewModel);
            _windowsRepository.Add(_dataBaseWindowViewModel, dataBaseWindowController);
        }

        private void CreateRequirementsWindow(RequirementsWindowData requirementsWindowData)
        {
            RequirementsWindowViewModel dataBaseWindowViewModel = new();
            WindowBaseAnimation windowBaseAnimation = new(
                requirementsWindowData.TargetRectTransform,
                requirementsWindowData.CanvasRectTransform,
                requirementsWindowData.DataBaseButtonTransform,
                requirementsWindowData.Duration);
            windowBaseAnimation.Initialize();

            BaseWindowController dataBaseWindowController = new(
                windowBaseAnimation,
                dataBaseWindowViewModel,
                requirementsWindowData.TargetRectTransform);

            requirementsWindowData.DataBaseWidget.Initialize(dataBaseWindowController, dataBaseWindowViewModel);
            requirementsWindowData.DataBaseCloseWidget.Initialize(dataBaseWindowController, dataBaseWindowViewModel);
            _windowsRepository.Add(dataBaseWindowViewModel, dataBaseWindowController);
        }
    }
}


