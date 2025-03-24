using Project.Common.Configs;
using UnityEngine;
using Zenject;

namespace Project.Common.Installers
{
    
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private ObjectsData _objectsData;
        [SerializeField] private PlayerRayCastData _playerRayCastData;
        
        public override void InstallBindings()
        {
            Container.Bind<ObjectsDataService>().AsSingle().WithArguments(_objectsData);
            Container.Bind<PlayerRayCastData>().FromInstance(_playerRayCastData).AsSingle();
        }
    }

}