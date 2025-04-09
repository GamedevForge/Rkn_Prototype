using Project.Common.Configs;
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

        public override void InstallBindings()
        {
            Container.Bind<ObjectsDataService>().AsSingle().WithArguments(_objectsData);
            Container.Bind<RequirementsDataService>().AsSingle().WithArguments(_requirementsData);
            Container.Bind<PlayerRayCastData>().FromInstance(_playerRayCastData).AsSingle();
            Container.Bind<DayHandler>().AsSingle();
            Container.BindInstance(DayNumber).WhenInjectedInto<DayHandler>();
        }
    }

}