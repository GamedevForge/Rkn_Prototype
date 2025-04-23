using UnityEngine;
using Zenject;
using Project.Common.Configs;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace Project.Common.Core
{
    public class DoorController : MonoBehaviour, IInteractableObject
    {
        [SerializeField] private Transform _animationTarget;
        [SerializeField] private float _animationDuration;
        
        private bool _openIsProcessing = false;
        private bool _isOpen = false;

        private ObjectsDataService _dataService;
        private Transform _playerTransform;
        private Quaternion _initialRotation;
        
        public bool CanInteract => _openIsProcessing == false;
        public InteractType InteractType => InteractType.Click;
        public float HoldTime => 0f;
        public string Name => _dataService.GetObjectConfig(ObjectType.AnimatedDoor).Name;

        [Inject] private void Construct(
            ObjectsDataService objectsDataService,
            PlayerComponents playerComponents)
        {
            _dataService = objectsDataService;
            _playerTransform = playerComponents.PlayerTransform;
        }

        private void Awake() =>
            _initialRotation = _animationTarget.rotation;

        public async void Interact()
        {
            _openIsProcessing = true;

            if (_isOpen)
            {
                await PlayCloseAnimationAsync();
                _isOpen = false;
            }
            else
            {
                await PlayOpenAnimationAsync();
                _isOpen = true;
            }

            _openIsProcessing = false;
        }
        
        private async UniTask PlayOpenAnimationAsync()
        {
            Tween tween;

            if (_playerTransform.rotation.y > 0f)
                tween = _animationTarget.DORotate(_animationTarget.eulerAngles + new Vector3(0f, -90f, 0f), _animationDuration);
            else
                tween = _animationTarget.DORotate(_animationTarget.eulerAngles + new Vector3(0f, 90f, 0f), _animationDuration);

            await tween.AsyncWaitForCompletion();
        }

        private async UniTask PlayCloseAnimationAsync() =>
            await _animationTarget.DORotate(_initialRotation.eulerAngles, _animationDuration).AsyncWaitForCompletion();
    }
}