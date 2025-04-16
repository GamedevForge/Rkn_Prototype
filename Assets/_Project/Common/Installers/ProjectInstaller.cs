using Project.Common.Configs;
using Project.Common.Core;
using Project.Common.Core.Quest;
using Project.Common.UI;
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

        public override void InstallBindings()
        {
            Container.Bind<ObjectsDataService>().AsSingle().WithArguments(_objectsData);
            Container.Bind<RequirementsDataService>().AsSingle().WithArguments(_requirementsData);
            Container.Bind<PlayerRayCastData>().FromInstance(_playerRayCastData).AsSingle();
            Container.Bind<DayHandler>().AsSingle();
            Container.Bind<NewsListData>().FromInstance(_newsListData).AsSingle();
            Container.BindInterfacesAndSelfTo<NewsModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameQuitController>().AsSingle();
            Container.BindInstance(DayNumber).WhenInjectedInto<DayHandler>();

            Container.BindInterfacesAndSelfTo<QuestConfigService>().AsSingle().WithArguments(_questData);
            Container.BindInterfacesAndSelfTo<QuestViewFactory>().AsSingle().WithArguments(_boardPrefab, _targetPointerPrefab, _uiElementPrefab);
            Container.BindInterfacesAndSelfTo<QuestViewController>().AsSingle();
            Container.BindInterfacesAndSelfTo<QuestController>().AsSingle();
        }
    }

}