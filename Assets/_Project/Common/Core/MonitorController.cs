using UnityEngine;
using Zenject;
using Project.Common.Configs;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using StarterAssets;

namespace Project.Common.Core
{
    public class MonitorController : MonoBehaviour, IInteractableObject
    {
        [SerializeField] private RectTransform _monitorCanvasRectTransform;
        [SerializeField] private float _duration;
        
        private ObjectsDataService _dataService;
        private PlayerState _playerState;
        private FirstPersonController _firstPersonController;
        private PlayerComponents _playerComponents;

        public bool CanInteract => _playerState.IsSitting && 
            _playerState.IsProcessing == false &&
            _playerState.InComputer == false;
        public string Name => _dataService.GetObjectConfig(ObjectType.Monitor).Name;

        [Inject] private void Construct(ObjectsDataService dataService,
            PlayerState playerState,
            FirstPersonController firstPersonController,
            PlayerComponents playerComponents)
        {
            _dataService = dataService;
            _playerState = playerState;
            _firstPersonController = firstPersonController;
            _playerComponents = playerComponents;
        }

        private void Awake() =>
            _playerState.OnStandUpAtComputer += QuitInMonitor;

        private void OnDestroy() =>
            _playerState.OnStandUpAtComputer -= QuitInMonitor;

        public async void Interact()
        {
            _playerState.EnableProcessing();
            _playerState.SitDownAtComputer();
            _playerState.DisableLooked();
            await SetLooked();
            await PlayMonitorAnimationAsync(0.0009f);
            _playerState.DisableProcessing();
        }

        private async void QuitInMonitor()
        {
            _playerState.EnableProcessing();
            await PlayMonitorAnimationAsync(0f);
            _playerState.EnableLooked();
            _playerState.DisableProcessing();
        }

        private async UniTask SetLooked()
        {
            Tween tween = _playerComponents
                .CameraTransform
                .DORotate(new Vector3(0f, 0f, 0f), _duration);

            await tween.AsyncWaitForCompletion();

            _firstPersonController.SetRotation(0f, _playerComponents.CameraTransform.rotation.eulerAngles.y * 2);
            _playerComponents.CameraTransform.rotation = Quaternion.identity;
        }

        private async UniTask PlayMonitorAnimationAsync(float offSet)
        {
            Tween tween = _monitorCanvasRectTransform.DOScaleY(offSet, _duration);
            await tween.AsyncWaitForCompletion();
        }
    }
}