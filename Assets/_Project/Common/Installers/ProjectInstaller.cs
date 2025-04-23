using Project.Common.Configs;
using Project.Common.Core;
using Project.Common.Core.Quest;
using Project.Common.UI;
using Unity.VisualScripting;
using UnityEngine;
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

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<DialogSignal>();

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
        }
    }
}