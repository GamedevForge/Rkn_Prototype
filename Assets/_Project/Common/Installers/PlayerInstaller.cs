using Project.Common.Configs;
using Project.Common.Core;
using Project.Common.Core.SaveLoadSystem;
using Project.Common.UI;
using StarterAssets;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using Zenject;

namespace Project.Common.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [Header("Player:")]
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private PlayerRayCastData _playerRayCastData;
        [SerializeField] private SceneSpawnPointData _sceneSpawnPointData;

        [Header("SaveLoadSystem:")]
        [SerializeField] private GameObject _saveLoadControllerPrefab;

        public override void InstallBindings()
        {
            Core.IFactory<GameObject, GameObject> playerFactory = new PlayerFactory();
            GameObject playerGameObject = playerFactory.Create(_playerPrefab);
            PlayerStateController playerStateController;

            SaveLoadModel saveLoadModel = new();

            PlayerRayCasterModel playerRayCasterModel = new();
            PlayerState playerState = new();

            FirstPersonController firstPersonController = playerGameObject.GetComponent<FirstPersonController>();
            CharacterController characterController = playerGameObject.GetComponent<CharacterController>();
            PlayerInteractController playerInteractController = playerGameObject.GetComponent<PlayerInteractController>();
            NavMeshAgent navMeshAgent = playerGameObject.GetComponent<NavMeshAgent>();
            PlayerComponents playerComponents = playerGameObject.GetComponent<PlayerComponents>();
            StarterAssetsInputs assetsInputs = playerGameObject.GetComponent<StarterAssetsInputs>();
            PlayerRayCasterController playerRayCasterController = playerGameObject.GetComponent<PlayerRayCasterController>();
            PlayerQuitController playerQuitController = playerGameObject.GetComponent<PlayerQuitController>();
            NewsController newsController = playerGameObject.GetComponent<NewsController>();
            PlayerPositionController playerPositionController = new(playerComponents, _sceneSpawnPointData);
            
            SaveLoadControllerFactory saveLoadControllerFactory = new(saveLoadModel, _saveLoadControllerPrefab, playerPositionController);
            SaveLoadController saveLoadController = saveLoadControllerFactory.Create();

            playerStateController = new PlayerStateController(
                firstPersonController,
                characterController,
                playerState);

            Container.Bind<PlayerStateController>().FromInstance(playerStateController).AsSingle();
            Container.BindInterfacesAndSelfTo<SaveLoadModel>().FromInstance(saveLoadModel).AsSingle();
            Container.BindInterfacesAndSelfTo<SaveLoadController>().FromInstance(saveLoadController).AsSingle();
            Container.Bind<SceneSpawnPointData>().FromInstance(_sceneSpawnPointData).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerState>().FromInstance(playerState).AsSingle();
            Container.Bind<FirstPersonController>().FromInstance(firstPersonController).AsSingle();
            Container.Bind<NavMeshAgent>().FromInstance(navMeshAgent).AsSingle();
            Container.Bind<StarterAssetsInputs>().FromInstance(assetsInputs).AsSingle();
            Container.Bind<PlayerComponents>().FromInstance(playerComponents).AsSingle();
            Container.Bind<CharacterController>().FromInstance(characterController).AsSingle();
            Container.Bind<NewsController>().FromInstance(newsController).AsSingle();
            Container.Bind<PlayerPositionController>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerRayCasterModel>().FromInstance(playerRayCasterModel).AsSingle();
            Container.BindInterfacesTo<PlayerActiveController>().AsSingle().WithArguments(playerGameObject);
            Container.Bind<PlayerInteractController>().FromInstance(playerInteractController).AsSingle();
            Container.Bind<PlayerRayCasterController>().FromInstance(playerRayCasterController).AsSingle();
            Container.Bind<PlayerQuitController>().FromInstance(playerQuitController).AsSingle();
            Container.BindInterfacesTo<SaveLoadPositionController>().AsSingle();

            Container.BindInterfacesTo<PlayerEntryPoint>().AsSingle();
        }
    }
}