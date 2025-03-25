using UnityEngine;
using Zenject;
using Project.Common.Configs;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using StarterAssets;

namespace Project.Common.Core
{
    public class ChairController : MonoBehaviour, IInteractableObject
    {
        [SerializeField] private Transform _endPointTransform;
        [SerializeField] private Transform _intermediateCameraPoint;
        [SerializeField] private Transform _cameraEndPoint;
        [SerializeField] private float _intermediateDuration;
        [SerializeField] private float _endDuration;

        private ObjectsDataService _dataService;
        private PlayerState _playerState;
        private PlayerComponents _playerComponents;
        private FirstPersonController _firstPersonController;

        public string Name => _dataService.GetObjectConfig(ObjectType.Chair).Name;
        public bool CanInteract => _playerState.IsSitting;

        [Inject] private void Construct(ObjectsDataService objectsDataService,
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
            _playerState.Sit();
            _playerComponents.Agent.enabled = true;
            _playerComponents.Agent.destination = _endPointTransform.position;

            await UniTask.WaitUntil(() => Mathf.Approximately(_endPointTransform.position.x, _playerComponents.PlayerTransform.position.x) &&
                Mathf.Approximately(_endPointTransform.position.z, _playerComponents.PlayerTransform.position.z));

            _playerComponents.Agent.enabled = false;

            await PlayCameraAnimationAsync();

            _firstPersonController.SetRotation(0f, _playerComponents.CameraTransform.rotation.eulerAngles.y * 2);
            _playerComponents.CameraTransform.rotation = Quaternion.identity;
            _playerState.EnableLooked();
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
}


