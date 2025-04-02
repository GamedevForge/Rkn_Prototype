using UnityEngine;
using Zenject;
using Project.Common.UI;
using StarterAssets;
using Project.Common.Core;
using UnityEngine.AI;
using Project.Common.Configs;
using UnityEngine.UI;
using static Project.Common.Core.EntryPoint;

namespace Project.Common.Installers
{
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField] private TextView _interactiveObjectsTextView;
        [SerializeField] private FirstPersonController _firstPersonController;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private PlayerInteractController _playerInteractController;
        [SerializeField] private PlayerRayCasterController _playerRayCasterController;
        [SerializeField] private NavMeshAgent _playerNavMeshAgent;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private PlayerQuitController _playerQuitController;
        [SerializeField] private CursorData _cursorData;
        [SerializeField] private StarterAssetsInputs _assetsInputs;
        [SerializeField] private RectTransform _cursorRectTransform;
        [SerializeField] private RectTransform _canvasRectTransform;

        [Header("Windows:")]
        [SerializeField] private WindowsData _windowsData;

        [Header("NewsObjects:")]
        [SerializeField] private BaseWidget _newsWindowWidget;
        [SerializeField] private CloseWidget _newsWindowCloseWidget;
        [SerializeField] private RectTransform _newsWindowRectTransform;
        [SerializeField] private RectTransform _newsButtonRectTransform;
        [SerializeField] private NewsListData _newsListData;
        [SerializeField] private Transform _armTransform;
        [SerializeField] private Transform _lookAtPointForCamera;
        [SerializeField] private Transform _approveButtonTransform;
        [SerializeField] private Transform _rejectButtonTransform;
        [SerializeField] private ApproveOrRejectData _approveOrRejectData;
        [SerializeField] private NewsController _newsController;
        [SerializeField] private Image _newsImage;

        [Header("DataBaseObjects")]
        [SerializeField] private BaseWidget _dataBaseWindowWidget;
        [SerializeField] private CloseWidget _dataBaseWindowCloseWidget;
        [SerializeField] private RectTransform _databaseWindowRectTransform;
        [SerializeField] private RectTransform _databaseButtonRectTransform;

        [Header("RequirementsObjects")]
        [SerializeField] private BaseWidget _requirementsWindowWidget;
        [SerializeField] private CloseWidget _requirementsWindowCloseWidget;
        [SerializeField] private RectTransform _requirementsWindowRectTransform;
        [SerializeField] private RectTransform _requirementsButtonRectTransform;

        public override void InstallBindings()
        {
            Container.Bind<PlayerInteractController>().FromInstance(_playerInteractController).AsSingle();
            Container.Bind<PlayerRayCasterController>().FromInstance(_playerRayCasterController).AsSingle();
            Container.Bind<FirstPersonController>().FromInstance(_firstPersonController).AsSingle();
            Container.Bind<CharacterController>().FromInstance(_characterController).AsSingle();
            Container.Bind<PlayerQuitController>().FromInstance(_playerQuitController).AsSingle();

            Container.Bind<PlayerRayCasterModel>().AsSingle();
            Container.Bind<PlayerState>().AsSingle();
            Container.Bind<PlayerComponents>().AsSingle().WithArguments(_playerNavMeshAgent, _cameraTransform, _playerTransform);
            Container.Bind<CursorAnimation>().AsSingle().WithArguments(_assetsInputs, _cursorRectTransform, _cursorData);
            Container.Bind<NewsWindowModel1>().AsSingle();
            Container.Bind<WindowsRepository>().AsSingle();

            Container.BindInterfacesTo<EntryPoint>().AsSingle().WithArguments(
                _interactiveObjectsTextView,
                new NewsWindowData
                {
                    News = _newsListData,
                    NewsWidget = _newsWindowWidget,
                    CanvasRectTransform = _canvasRectTransform,
                    NewsButtonTransform = _newsButtonRectTransform,
                    TargetRectTransform = _newsWindowRectTransform,
                    NewsController = _newsController,
                    ApproveButtonTransform = _approveButtonTransform,
                    RejectButtonTransform = _rejectButtonTransform,
                    CameraLookAtPointTransform = _lookAtPointForCamera,
                    ArmTransform = _armTransform,
                    ApproveOrRejectData = _approveOrRejectData,
                    NewsImage = _newsImage,
                    Duration = _windowsData.AnimationDuration,
                    NewsCloseWidget = _newsWindowCloseWidget,
                },
                new DataBaseWindowData
                {
                    TargetRectTransform = _databaseWindowRectTransform,
                    CanvasRectTransform = _canvasRectTransform,
                    DataBaseButtonTransform = _databaseButtonRectTransform,
                    DataBaseWidget = _dataBaseWindowWidget,
                    Duration = _windowsData.AnimationDuration,
                    DataBaseCloseWidget = _dataBaseWindowCloseWidget,
                },
                new RequirementsWindowData
                {
                    TargetRectTransform = _requirementsWindowRectTransform,
                    CanvasRectTransform = _canvasRectTransform,
                    DataBaseButtonTransform = _requirementsButtonRectTransform,
                    DataBaseWidget = _requirementsWindowWidget,
                    Duration = _windowsData.AnimationDuration,
                    DataBaseCloseWidget = _requirementsWindowCloseWidget,
                });
        }
    }
}