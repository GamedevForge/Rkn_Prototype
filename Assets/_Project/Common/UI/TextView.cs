using Cysharp.Threading.Tasks;
using DG.Tweening;
using Project.Common.Configs;
using Project.Common.Core;
using StarterAssets;
using TMPro;
using UnityEngine;

namespace Project.Common.UI
{
    [RequireComponent (typeof(TMP_Text))]
    public class TextView : MonoBehaviour
    {
        private TMP_Text Text => GetComponent<TMP_Text>();

        public void DrawText(string text) =>
            Text.text = text;
    }

    public class NewsModel
    {
        private readonly NewsListData _data;

        public NewsConfig CurrentNews { get; private set; }

        public NewsModel(NewsListData data)
        {
            _data = data;
        }

        public void ChangeCurrentNews() =>
            CurrentNews = _data.NewsConfigs[Random.Range(0, _data.NewsConfigs.Length + 1)];
    }

    public class NewsController : MonoBehaviour
    {
        private NewsModel _model;
        private IWindowWithSprite _view;
        private NewsApproveOrRejectAnimations _animationController;
        private PlayerState _playerState;

        public void Initialize(
            NewsModel newsModel, 
            IWindowWithSprite newsViewModel,
            NewsApproveOrRejectAnimations newsApproveOrRejectAnimations,
            PlayerState playerState)
        {
            _model = newsModel;
            _view = newsViewModel;
            _animationController = newsApproveOrRejectAnimations;
            _playerState = playerState;
        }

        public async void OnApprove()
        {
            ChangeNews();
        }

        public async void OnReject()
        {
            ChangeNews();
        }

        private void ChangeNews()
        {
            _model.ChangeCurrentNews();
            _view.ChangeNewsSprite(_model.CurrentNews.Sprite);
        }
    }

    public class NewsApproveOrRejectAnimations
    {
        private readonly Transform _cameraTransform;
        private readonly Transform _armTransform;
        private readonly Transform _cameraLookAtPosition;
        private readonly Transform _approveButtonTransform;
        private readonly Transform _rejectButtonTransform;
        private readonly ApproveOrRejectData _approveOrRejectData;
        private readonly FirstPersonController _firstPersonController;
        private readonly PlayerState _playerState;

        public NewsApproveOrRejectAnimations(
            Transform armTransform,
            PlayerComponents playerComponents, 
            Transform cameraLookAtPosition,
            ApproveOrRejectData approveOrRejectData,
            Transform approveButtonTransform,
            Transform rejectButtonTransform,
            FirstPersonController firstPersonController,
            PlayerState playerState)
        {
            _armTransform = armTransform;
            _cameraTransform = playerComponents.CameraTransform;
            _cameraLookAtPosition = cameraLookAtPosition;
            _approveOrRejectData = approveOrRejectData;
            _approveButtonTransform = approveButtonTransform;
            _rejectButtonTransform = rejectButtonTransform;
            _firstPersonController = firstPersonController;
            _playerState = playerState;
        }

        public UniTask PlayApproveAnimationAsync() =>
            PlayOriginAnimation(new Vector3(
                _approveButtonTransform.position.x,
                _armTransform.position.y, 
                _armTransform.position.z));

        public UniTask PlayRejectAnimationAsync() =>
            PlayOriginAnimation(new Vector3(
                _rejectButtonTransform.position.x,
                _armTransform.position.y,
                _armTransform.position.z));

        private async UniTask PlayOriginAnimation(Vector3 armPosition)
        {
            _playerState.EnableProcessing();
            _armTransform.position = armPosition;

            await PlayCameraLookAtAnimationAsync();
            await PlayArmAnimationAsync();
            await PlayCameraLookAtOriginPointAnimationAsync();

            _playerState.DisableProcessing();
        }

        private async UniTask PlayArmAnimationAsync()
        {
            Tween tween = _armTransform.DORotate(Vector3.zero, _approveOrRejectData.ArmAnimationDuration);
            await tween.AsyncWaitForCompletion();

            tween = _armTransform.DORotate(new Vector3(90f, 0f, 0f), _approveOrRejectData.ArmAnimationDuration);
            await tween.AsyncWaitForCompletion();
        }

        private async UniTask PlayCameraLookAtAnimationAsync()
        {
            Tween tween = _cameraTransform.DOLookAt(
                _cameraLookAtPosition.position, 
                _approveOrRejectData.CameraAnimationDuration);
            await tween.AsyncWaitForCompletion();
        }
        private async UniTask PlayCameraLookAtOriginPointAnimationAsync()
        {
            Tween tween = _cameraTransform.DORotate(new Vector3(0f, 0f, 0f), _approveOrRejectData.ArmAnimationDuration);

            await tween.AsyncWaitForCompletion();

            _firstPersonController.SetRotation(0f, _cameraTransform.rotation.eulerAngles.y * 2);
            _cameraTransform.rotation = Quaternion.identity;
        }
    }
}
