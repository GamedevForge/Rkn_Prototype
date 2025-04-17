using UnityEngine;
using Zenject;
using Project.Common.Configs;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using StarterAssets;
using System;

namespace Project.Common.Core
{
    public class ChairController : MonoBehaviour, IInteractableObject, IQuestEvent<Transform>
    {
        public event Action OnEvent;
        
        [SerializeField] private Transform _endPointTransform;
        [SerializeField] private Transform _intermediateCameraPoint;
        [SerializeField] private float _intermediateDuration;
        [SerializeField] private float _endDuration;

        private ObjectsDataService _dataService;
        private PlayerState _playerState;
        private PlayerComponents _playerComponents;
        private FirstPersonController _firstPersonController;

        [field: SerializeField] public float HoldTime { get; private set; } 
        [field: SerializeField] public Transform MarkerTarget { get; private set; }
        public InteractType InteractType => InteractType.Click;
        public string Name => _dataService.GetObjectConfig(ObjectType.Chair).Name;
        public bool CanInteract => _playerState.IsSitting ||
            _playerState.IsProcessing == false;

        [Inject] private void Construct(
            ObjectsDataService objectsDataService,
            PlayerState playerState,
            PlayerComponents playerComponents,
            FirstPersonController firstPersonController)
        {
            _dataService = objectsDataService;
            _playerState = playerState;
            _playerComponents = playerComponents;
            _firstPersonController = firstPersonController;
        }

        public async void Interact()
        {
            OnEvent?.Invoke();
            _playerState.Sit();
            _playerState.EnableProcessing();
            _playerComponents.Agent.enabled = true;
            _playerComponents.Agent.destination = _endPointTransform.position;

            await UniTask.WaitUntil(() => Mathf.Approximately(_endPointTransform.position.x, _playerComponents.PlayerTransform.position.x) &&
                Mathf.Approximately(_endPointTransform.position.z, _playerComponents.PlayerTransform.position.z));

            _playerComponents.Agent.enabled = false;

            await PlayCameraAnimationAsync();

            _firstPersonController.SetRotation(0f, _playerComponents.CameraTransform.rotation.eulerAngles.y * 2);
            _playerComponents.CameraTransform.rotation = Quaternion.identity;
            _playerState.EnableLooked();
            _playerState.DisableProcessing();
        }

        private async UniTask PlayCameraAnimationAsync()
        {
            Tween intermediateTween;
            Tween endTween;

            intermediateTween = _playerComponents
                .CameraTransform
                .DOLookAt(_intermediateCameraPoint.position, _intermediateDuration);

            await intermediateTween.AsyncWaitForCompletion();

            endTween = _playerComponents
                .CameraTransform
                .DORotate(new Vector3(0f, 0f, 0f), _endDuration);

            await endTween.AsyncWaitForCompletion();
        }
    }

    public class DialogStartController : MonoBehaviour, IInteractableObject
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _animationDuration;
        
        private ObjectsDataService _objectsDataService;
        private PlayerComponents _playerComponents;
        private PlayerState _playerState;
        private DialogModel _dialogModel;
        private DialogController _dialogController;

        public bool CanInteract => true;
        public InteractType InteractType => InteractType.Click;
        public float HoldTime => 0f;
        public string Name => _objectsDataService.GetObjectConfig(ObjectType.Dialog).Name;

        [Inject] private void Construct(
            ObjectsDataService objectsDataService,
            PlayerComponents playerComponents,
            PlayerState playerState,
            DialogModel dialogModel,
            DialogController dialogController)
        {
            _objectsDataService = objectsDataService;
            _playerComponents = playerComponents;
            _playerState = playerState;
            _dialogModel = dialogModel;
            _dialogController = dialogController;
        }

        public async void Interact()
        {
            _playerState.Sit();
            _playerState.DisableLooked();

            await PlayLookAtTargetAnimationAsync();

            _dialogController.StartDialog();
            await UniTask.WaitWhile(() => _dialogModel.DialogIsProcessing);
        }

        private async UniTask PlayLookAtTargetAnimationAsync()
        {
            Tween tween;

            tween = _playerComponents
                .CameraTransform
                .DOLookAt(_target.position, _animationDuration);

            await tween.AsyncWaitForCompletion();
        }
    }

    public class DialogController
    {
        private readonly DialogModel _dialogModel;

        public DialogController(DialogModel dialogModel)
        {
            _dialogModel = dialogModel;
        }

        public void StartDialog()
        {
            _dialogModel.SetDialogState(true);

        }

        public void StopDialog()
        {
            _dialogModel.SetDialogState(false);

        }
    }

    public class DialogModel
    {
        public event Action<bool> OnDialogStateChange;
        
        public bool DialogIsProcessing { get; private set; } = false;

        public void SetDialogState(bool state)
        {
            DialogIsProcessing = state;
            OnDialogStateChange?.Invoke(state);
        }
    }

    public class DialogViewController
    {

    }

    public class DialogViewFactory
    {
        
    }
}