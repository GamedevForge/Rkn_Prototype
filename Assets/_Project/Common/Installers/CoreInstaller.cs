using UnityEngine;
using Zenject;
using Project.Common.UI;
using StarterAssets;
using Project.Common.Core;

namespace Project.Common.Installers
{
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField] private TextView _interactiveObjectsTextView;
        [SerializeField] private FirstPersonController _firstPersonController;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private PlayerInteractController _playerInteractController;
        [SerializeField] private PlayerRayCasterController _playerRayCasterController;
    
        public override void InstallBindings()
        {
            Container.Bind<PlayerInteractController>().FromInstance(_playerInteractController).AsSingle();
            Container.Bind<PlayerRayCasterController>().FromInstance(_playerRayCasterController).AsSingle();
            Container.Bind<FirstPersonController>().FromInstance(_firstPersonController).AsSingle();
            Container.Bind<CharacterController>().FromInstance(_characterController).AsSingle();

            Container.Bind<InteractiveObjectsTextController>().AsSingle().WithArguments(_interactiveObjectsTextView);
            Container.Bind<PlayerRayCasterModel>().AsSingle();
            Container.Bind<PlayerState>().AsSingle();
            
            Container.BindInterfacesTo<EntryPoint>().AsSingle();
        }
    }
}