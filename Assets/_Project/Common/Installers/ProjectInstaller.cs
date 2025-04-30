using Project.Common.Configs;
using Project.Common.Core;
using Project.Common.Core.Quest;
using Project.Common.Core.SaveLoadSystem;
using Project.Common.UI;
using StarterAssets;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Project.Common.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [InjectOptional]
        public int DayNumber;

        [SerializeField] private ObjectsData _objectsData;
        [SerializeField] private PlayerRayCastData _playerRayCastData;
        [SerializeField] private RequirementsData _requirementsData;
        [SerializeField] private NewsListData _newsListData;

        [Header("SaveLoadSystem:")]
        [SerializeField] private GameObject _saveLoadControllerPrefab;

        [Header("SpawnPoints:")]
        [SerializeField] private SceneSpawnPointData _sceneSpawnPointData;

        [Header("Quest:")]
        [SerializeField] private GameObject _boardPrefab;
        [SerializeField] private GameObject _targetPointerPrefab;
        [SerializeField] private GameObject _uiElementPrefab;
        [SerializeField] private QuestData _questData;

        [Header("Dialog:")]
        [SerializeField] private GameObject _dialogBoard;
        [SerializeField] private GameObject _dialogButton;
        [SerializeField] private GameObject _dialogText;
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private DialogListData _dialogListData;
        [SerializeField] private NPCDataList _npcDataList;

        [Header("Other:")]
        [SerializeField] private GameObject _widescreenPrefab;

        [Header("Player:")]
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private InteractableObjectsView _interactiveObjectsTextView;
        [SerializeField] private FirstPersonController _firstPersonController;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private PlayerInteractController _playerInteractController;
        [SerializeField] private PlayerRayCasterController _playerRayCasterController;
        [SerializeField] private NavMeshAgent _playerNavMeshAgent;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private PlayerQuitController _playerQuitController;
        [SerializeField] private StarterAssetsInputs _assetsInputs;
        [SerializeField] private PlayerTransform _playerTransformOnDestroy;

        public override void InstallBindings()
        {
            SaveLoadModel saveLoadModel = new();
            SaveLoadControllerFactory saveLoadControllerFactory = new(saveLoadModel, _saveLoadControllerPrefab);
            SaveLoadController saveLoadController = saveLoadControllerFactory.Create();

            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<DialogSignal>();

            Container.BindInterfacesAndSelfTo<GameState>().AsSingle();
            Container.BindInterfacesAndSelfTo<SaveLoadModel>().FromInstance(saveLoadModel).AsSingle();
            Container.BindInterfacesAndSelfTo<SaveLoadController>().FromInstance(saveLoadController).AsSingle();

            Container.Bind<SceneSpawnPointData>().FromInstance(_sceneSpawnPointData).AsSingle();
            Container.Bind<SaveLoadControllerFactory>().AsSingle().WithArguments(_saveLoadControllerPrefab);
            Container.BindInterfacesAndSelfTo<PlayerWorldPosition>().AsSingle();
            Container.Bind<ObjectsDataService>().AsSingle().WithArguments(_objectsData);
            Container.Bind<RequirementsDataService>().AsSingle().WithArguments(_requirementsData);
            Container.Bind<PlayerRayCastData>().FromInstance(_playerRayCastData).AsSingle();
            Container.Bind<DayHandler>().AsSingle();
            Container.Bind<NewsListData>().FromInstance(_newsListData).AsSingle();
            Container.BindInterfacesAndSelfTo<NewsModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameQuitController>().AsSingle();
            Container.BindInstance(DayNumber).WhenInjectedInto<DayHandler>();

            Container.Bind<CanvasRepository>().AsSingle();
            Container.BindInterfacesAndSelfTo<WidesreenFactory>().AsSingle();
            Container.Bind<WidescreenController>().AsSingle();

            Container.BindInterfacesAndSelfTo<QuestConfigService>().AsSingle().WithArguments(_questData);
            Container.BindInterfacesAndSelfTo<QuestViewFactory>().AsSingle().WithArguments(_boardPrefab, _targetPointerPrefab, _uiElementPrefab);
            Container.BindInterfacesAndSelfTo<QuestViewController>().AsSingle();
            Container.BindInterfacesAndSelfTo<QuestController>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerData>().FromInstance(_playerData).AsSingle();
            Container.Bind<NPCDataService>().AsSingle().WithArguments(_npcDataList);
            Container.Bind<DialogDataService>().AsSingle().WithArguments(_dialogListData);
            Container.Bind<DialogModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<DialogViewFactory>().AsSingle().WithArguments(_dialogBoard, _dialogText, _dialogButton);
            Container.BindInterfacesAndSelfTo<DialogViewController>().AsSingle();
            Container.BindInterfacesAndSelfTo<DialogController>().AsSingle();

            Container.BindInterfacesTo<ProjectEntryPoint>().AsSingle().WithArguments(new WidescreenData { Prefab = _widescreenPrefab} );

            Container.Bind<PlayerInteractController>().FromInstance(_playerInteractController).AsSingle();
            Container.Bind<PlayerRayCasterController>().FromInstance(_playerRayCasterController).AsSingle();
            Container.Bind<FirstPersonController>().FromInstance(_firstPersonController).AsSingle();
            Container.Bind<CharacterController>().FromInstance(_characterController).AsSingle();
            Container.Bind<PlayerQuitController>().FromInstance(_playerQuitController).AsSingle();
            Container.Bind<StarterAssetsInputs>().FromInstance(_assetsInputs).AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerRayCasterModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerState>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerComponents>().AsSingle().WithArguments(_playerNavMeshAgent, _cameraTransform, _playerTransform, _playerTransformOnDestroy);
        }
    }
}