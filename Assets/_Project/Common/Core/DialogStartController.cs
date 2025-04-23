using UnityEngine;
using Zenject;
using Project.Common.Configs;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using StarterAssets;

namespace Project.Common.Core
{
    public class DialogStartController : MonoBehaviour, IInteractableObject
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _npcRotationTarget;
        [SerializeField] private float _animationDuration;
        [SerializeField] private float _npcAnimationDuration;
        [SerializeField] private string _id;

        private ObjectsDataService _objectsDataService;
        private PlayerComponents _playerComponents;
        private PlayerState _playerState;
        private DialogModel _dialogModel;
        private DialogController _dialogController;
        private StarterAssetsInputs _starterAssetsInputs;

        private bool _dialogIsProcessing = false;

        public bool CanInteract => _dialogIsProcessing == false;
        public InteractType InteractType => InteractType.Click;
        public float HoldTime => 0f;
        public string Name => _objectsDataService.GetObjectConfig(ObjectType.Dialog).Name;

        [Inject] private void Construct(
            ObjectsDataService objectsDataService,
            PlayerComponents playerComponents,
            PlayerState playerState,
            DialogModel dialogModel,
            DialogController dialogController,
            StarterAssetsInputs starterAssetsInputs)
        {
            _objectsDataService = objectsDataService;
            _playerComponents = playerComponents;
            _playerState = playerState;
            _dialogModel = dialogModel;
            _dialogController = dialogController;
            _starterAssetsInputs = starterAssetsInputs;
        }

        public async void Interact()
        {
            _dialogIsProcessing = true;
            _playerState.Sit();
            _playerState.DisableLooked();

            await UniTask.WhenAll(PlayLookAtTargetAnimationAsync(), PlayLookAtPlayerAnimationAsync());

            _starterAssetsInputs.ShowCursor();
            await _dialogController.StartDialog(_id);
            await UniTask.WaitWhile(() => _dialogModel.DialogIsProcessing);

            _starterAssetsInputs.HideCursor();
            _playerState.EnableLooked();
            _playerState.StandUp();
            _playerState.DisableProcessing();
            _dialogIsProcessing = false;
        }

        private async UniTask PlayLookAtTargetAnimationAsync()
        {
            await _playerComponents
                .CameraTransform
                .DOLookAt(_target.position, _animationDuration).AsyncWaitForCompletion();
        }

        private async UniTask PlayLookAtPlayerAnimationAsync()
        {
            await _npcRotationTarget
                .DOLookAt(_playerComponents.PlayerTransform.position, _npcAnimationDuration, AxisConstraint.None, Vector3.up)
                .AsyncWaitForCompletion();
        }
    }
}