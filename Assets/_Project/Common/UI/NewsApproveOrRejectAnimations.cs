using Cysharp.Threading.Tasks;
using DG.Tweening;
using Project.Common.Configs;
using Project.Common.Core;
using StarterAssets;
using UnityEngine;

namespace Project.Common.UI
{
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
            _armTransform.gameObject.SetActive(true);
            _armTransform.position = armPosition;

            await PlayCameraLookAtAnimationAsync();
            await PlayArmAnimationAsync();
            await PlayCameraLookAtOriginPointAnimationAsync();

            _armTransform.gameObject.SetActive(false);
            _playerState.DisableProcessing();
        }

        private async UniTask PlayArmAnimationAsync()
        {
            Tween tween = _armTransform.DORotate(Vector3.zero, _approveOrRejectData.ArmAnimationDuration);
            await tween.AsyncWaitForCompletion();

            tween = _armTransform.DORotate(new Vector3(-90f, 0f, 0f), _approveOrRejectData.ArmAnimationDuration);
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
