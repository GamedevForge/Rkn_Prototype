using UnityEngine;
using Zenject;
using Project.Common.UI;
using StarterAssets;
using Project.Common.Core;
using UnityEngine.AI;
using Project.Common.Configs;
using UnityEngine.UI;

namespace Project.Common.Installers
{
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField] private InteractableObjectsView _interactiveObjectsTextView;
        //[SerializeField] private FirstPersonController _firstPersonController;
        //[SerializeField] private CharacterController _characterController;
        //[SerializeField] private PlayerInteractController _playerInteractController;
        //[SerializeField] private PlayerRayCasterController _playerRayCasterController;
        //[SerializeField] private NavMeshAgent _playerNavMeshAgent;
        //[SerializeField] private Transform _cameraTransform;
        //[SerializeField] private Transform _playerTransform;
        //[SerializeField] private PlayerQuitController _playerQuitController;
        [SerializeField] private CursorData _cursorData;
        //[SerializeField] private StarterAssetsInputs _assetsInputs;
        [SerializeField] private RectTransform _cursorRectTransform;
        [SerializeField] private RectTransform _canvasRectTransform;
        //[SerializeField] private PlayerTransform _playerTransformOnDestroy;

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
        [SerializeField] private GameObject _newsGameObject;
        [SerializeField] private GameObject _screenIfNewsIsOver;

        [Header("DataBaseObjects")]
        [SerializeField] private BaseWidget _dataBaseWindowWidget;
        [SerializeField] private CloseWidget _dataBaseWindowCloseWidget;
        [SerializeField] private RectTransform _databaseWindowRectTransform;
        [SerializeField] private RectTransform _databaseButtonRectTransform;
        [SerializeField] private InquiriesData _inquiriesData;
        [SerializeField] private TMPro.TMP_Text _textInfo;
        [SerializeField] private GameObject _buttonPrefab;
        [SerializeField] private Transform _buttonPrefabsParent;
        [SerializeField] private Transform _secondParent;
        [SerializeField] private DataBaseController _dataBaseController;

        [Header("RequirementsObjects")]
        [SerializeField] private BaseWidget _requirementsWindowWidget;
        [SerializeField] private CloseWidget _requirementsWindowCloseWidget;
        [SerializeField] private TMPro.TMP_Text __requirementsText;
        [SerializeField] private RectTransform _requirementsWindowRectTransform;
        [SerializeField] private RectTransform _requirementsButtonRectTransform;

        public override void InstallBindings()
        {
            Container.Bind<CursorAnimation>().AsSingle().WithArguments(_cursorRectTransform, _cursorData);
            Container.Bind<InteractableObjectsView>().FromInstance(_interactiveObjectsTextView).AsSingle();
            Container.Bind<NewsWindowModel1>().AsSingle();
            Container.Bind<WindowsRepository>().AsSingle();

            Container.BindInterfacesTo<EntryPoint>().AsSingle().WithArguments(
                new NewsWindowData
                {
                    News = _newsListData,
                    NewsWidget = _newsWindowWidget,
                    CanvasRectTransform = _canvasRectTransform,
                    NewsButtonTransform = _newsButtonRectTransform,
                    TargetRectTransform = _newsWindowRectTransform,
                    ApproveButtonTransform = _approveButtonTransform,
                    RejectButtonTransform = _rejectButtonTransform,
                    CameraLookAtPointTransform = _lookAtPointForCamera,
                    ArmTransform = _armTransform,
                    ApproveOrRejectData = _approveOrRejectData,
                    NewsImage = _newsImage,
                    Duration = _windowsData.AnimationDuration,
                    NewsCloseWidget = _newsWindowCloseWidget,
                    NewsGameObject = _newsGameObject,
                    ScreenIfNewsIsOver = _screenIfNewsIsOver,
                },
                new DataBaseWindowData
                {
                    TargetRectTransform = _databaseWindowRectTransform,
                    CanvasRectTransform = _canvasRectTransform,
                    DataBaseButtonTransform = _databaseButtonRectTransform,
                    DataBaseWidget = _dataBaseWindowWidget,
                    Duration = _windowsData.AnimationDuration,
                    DataBaseCloseWidget = _dataBaseWindowCloseWidget,
                    InquiriesData = _inquiriesData,
                    TextInfo = _textInfo,
                    ButtonPrefab = _buttonPrefab,
                    ButtonPrefabsParent = _buttonPrefabsParent,
                    DataBaseController = _dataBaseController,
                    SecondParent = _secondParent,
                },
                new RequirementsWindowData
                {
                    TargetRectTransform = _requirementsWindowRectTransform,
                    CanvasRectTransform = _canvasRectTransform,
                    DataBaseButtonTransform = _requirementsButtonRectTransform,
                    DataBaseWidget = _requirementsWindowWidget,
                    Duration = _windowsData.AnimationDuration,
                    DataBaseCloseWidget = _requirementsWindowCloseWidget,
                    Text = __requirementsText,
                });
        }
    }
}