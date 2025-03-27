using UnityEngine;
using Zenject;
using Project.Common.UI;
using StarterAssets;
using Project.Common.Core;
using UnityEngine.AI;
using Project.Common.Configs;

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
            Container.Bind<NewsWindowModel>().AsSingle();

            Container.BindInterfacesTo<EntryPoint>().AsSingle().WithArguments(_interactiveObjectsTextView);
        }
    }
}