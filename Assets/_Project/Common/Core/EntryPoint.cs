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
        private readonly InteractiveObjectsTextController _textController;
        private readonly FirstPersonController _firstPersonController;
        private readonly CursorAnimation _cursorAnimation;
        private readonly PlayerComponents _playerComponents;
        private readonly WindowsRepository _windowsRepository;
        private readonly IInstantiator _instantiator;
        private readonly NewsModel _newsModel;
        private readonly NewsController _newsController;

        private DataBaseWindowViewModel _dataBaseWindowViewModel;
        private DataBaseViewController _dataBaseViewController;

        public EntryPoint(PlayerState playerState,
            FirstPersonController firstPersonController,
            NewsModel newsModel,
            IInstantiator instantiator,
            WindowsRepository windowsRepository,
            PlayerComponents playerComponents,
            CursorAnimation cursorAnimation,
            NewsWindowData newsWindowData,
            DataBaseWindowData dataBaseWindowData,
            RequirementsWindowData requirementsWindowData,
            NewsController newsController,
            InteractableObjectsView interactableObjectsView,
            PlayerRayCasterModel playerRayCasterModel)
        {
            _playerState = playerState;
            _playerComponents = playerComponents;
            _instantiator = instantiator;
            _newsModel = newsModel;
            _newsController = newsController;
            _textController = new InteractiveObjectsTextController(interactableObjectsView, playerRayCasterModel);
            
            _firstPersonController = firstPersonController;
            _cursorAnimation = cursorAnimation;
            _windowsRepository = windowsRepository;

            CreateNewsWindow(newsWindowData);
            CreateDataBaseWindow(dataBaseWindowData);
            CreateRequirementsWindow(requirementsWindowData);
            _windowsRepository.Initialize();
        }

        public void Initialize()
        {
            //_interactController.Initialize(_rayCasterModel);
            //_rayCasterController.Initialize(_rayCasterModel);
            //_playerStateController.Initialize();
            _textController.Initialize();
            //_playerQuitController.Initialize(_playerState, _firstPersonController);
            _cursorAnimation.Initialize();
        }

        public void Dispose()
        {
            _textController.Dispose();
            _cursorAnimation.Dispose();
            _dataBaseWindowViewModel.Dispose();
            _dataBaseWindowViewModel.Dispose();
        }

        private void CreateNewsWindow(NewsWindowData newsWindowData)
        {
            NewsWindowViewModel newsWindowViewModel = new(
                newsWindowData.NewsImage, 
                newsWindowData.NewsGameObject, 
                newsWindowData.ScreenIfNewsIsOver);            
            WindowBaseAnimation windowBaseAnimation = new(
                newsWindowData.TargetRectTransform,
                newsWindowData.CanvasRectTransform,
                newsWindowData.NewsButtonTransform,
                newsWindowData.Duration);
            
            windowBaseAnimation.Initialize();
            NewsWindowController newsWindowController = new(windowBaseAnimation, newsWindowViewModel, newsWindowData.TargetRectTransform, _playerState);
            newsWindowData.NewsWidget.Initialize(newsWindowController, newsWindowViewModel);
            newsWindowData.NewsCloseWidget.Initialize(newsWindowController, newsWindowViewModel);

            NewsApproveOrRejectAnimations animation = new(
                newsWindowData.ArmTransform,
                _playerComponents,
                newsWindowData.CameraLookAtPointTransform,
                newsWindowData.ApproveOrRejectData,
                newsWindowData.ApproveButtonTransform,
                newsWindowData.RejectButtonTransform,
                _firstPersonController,
                _playerState);
            _newsController.SetNews(_newsModel, newsWindowViewModel, animation);
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
                repository,
                dataBaseWindowData.SecondParent);
            WindowBaseAnimation windowBaseAnimation = new(
                dataBaseWindowData.TargetRectTransform,
                dataBaseWindowData.CanvasRectTransform,
                dataBaseWindowData.DataBaseButtonTransform,
                dataBaseWindowData.Duration);
            windowBaseAnimation.Initialize();
            _dataBaseWindowViewModel.Initialize();
            _dataBaseViewController.Initialize();
            dataBaseWindowData.DataBaseController.Initialize(dataBaseModel, searchEngine);

            DataBaseWindowController dataBaseWindowController = new(
                windowBaseAnimation,
                _dataBaseWindowViewModel,
                dataBaseWindowData.TargetRectTransform,
                repository);

            dataBaseWindowData.DataBaseWidget.Initialize(dataBaseWindowController, _dataBaseWindowViewModel);
            dataBaseWindowData.DataBaseCloseWidget.Initialize(dataBaseWindowController, _dataBaseWindowViewModel);
            _windowsRepository.Add(_dataBaseWindowViewModel, dataBaseWindowController);
        }

        private void CreateRequirementsWindow(RequirementsWindowData requirementsWindowData)
        {
            RequirementsModel model = _instantiator.Instantiate<RequirementsModel>();
            RequirementsWindowViewModel dataBaseWindowViewModel = new(model);
            WindowBaseAnimation windowBaseAnimation = new(
                requirementsWindowData.TargetRectTransform,
                requirementsWindowData.CanvasRectTransform,
                requirementsWindowData.DataBaseButtonTransform,
                requirementsWindowData.Duration);
            windowBaseAnimation.Initialize();

            RequirementsWindowViewController dataBaseWindowController = new(
                windowBaseAnimation,
                dataBaseWindowViewModel,
                requirementsWindowData.TargetRectTransform,
                requirementsWindowData.Text);

            dataBaseWindowController.Initialize();
            requirementsWindowData.DataBaseWidget.Initialize(dataBaseWindowController, dataBaseWindowViewModel);
            requirementsWindowData.DataBaseCloseWidget.Initialize(dataBaseWindowController, dataBaseWindowViewModel);
            _windowsRepository.Add(dataBaseWindowViewModel, dataBaseWindowController);
        }
    }
}


