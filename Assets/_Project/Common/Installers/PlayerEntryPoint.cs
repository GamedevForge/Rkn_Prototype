using Project.Common.Configs;
using Project.Common.Core;
using Project.Common.Core.Quest;
using Project.Common.UI;
using StarterAssets;
using Zenject;

namespace Project.Common.Installers
{
    public class PlayerEntryPoint : IInitializable
    {
        private readonly PlayerInteractController _playerInteractController;
        private readonly PlayerRayCasterController _playerRayCastController;
        private readonly PlayerStateController _playerStateController;  
        private readonly PlayerQuitController _playerQuitController;
        private readonly StarterAssetsInputs _assetsInputs;
        private readonly NewsController _newsController;
        private readonly PlayerRayCasterModel _playerRayCastModel;
        private readonly PlayerRayCastData _playerRayCastData;
        private readonly PlayerState _playerState;
        private readonly FirstPersonController _firstPersonController;
        private readonly PlayerComponents _playerComponents;
        private readonly QuestController _questController;

        public PlayerEntryPoint(
            PlayerInteractController playerInteractController,
            PlayerRayCasterController playerRayCasterController,
            PlayerStateController playerStateController,
            PlayerQuitController playerQuitController,
            StarterAssetsInputs starterAssetsInputs,
            NewsController newsController,
            PlayerRayCasterModel playerRayCasterModel,
            PlayerRayCastData playerRayCastData,
            PlayerState playerState,
            FirstPersonController firstPersonController,
            PlayerComponents playerComponents,
            QuestController questController) 
        { 
            _playerInteractController = playerInteractController;
            _playerRayCastController = playerRayCasterController;
            _playerStateController = playerStateController;
            _playerQuitController = playerQuitController;
            _assetsInputs = starterAssetsInputs;
            _newsController = newsController;
            _playerRayCastModel = playerRayCasterModel;
            _playerRayCastData = playerRayCastData;
            _playerState = playerState;
            _firstPersonController = firstPersonController;
            _playerComponents = playerComponents;
            _questController = questController;
        }

        public void Initialize()
        {
            _playerInteractController.Initialize(_playerRayCastModel);
            _playerRayCastController.Initialize(_playerRayCastModel, _playerRayCastData);
            _playerStateController.Initialize();
            _playerQuitController.Initialize(_playerState, _firstPersonController);
            _assetsInputs.Initialize(_playerState);
            _newsController.Initialize(_playerState);
            foreach (var questEvent in _playerComponents.QuestEvents)
                questEvent.SetQuestController(_questController);
        }
    }
}