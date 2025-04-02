using Project.Common.Core;
using UnityEngine;

namespace Project.Common.UI
{
    public class NewsController : MonoBehaviour
    {
        private NewsModel _model;
        private IWindowWithSprite _view;
        private NewsApproveOrRejectAnimations _animationController;
        private PlayerState _playerState;

        private bool IsPossible => _playerState.IsProcessing == false &&
            _playerState.IsSitting &&
            _playerState.InComputer;

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
            if (IsPossible == false)
                return;
            
            await _animationController.PlayApproveAnimationAsync();
            ChangeNews();
        }

        public async void OnReject()
        {
            if (IsPossible == false)
                return;

            await _animationController.PlayRejectAnimationAsync();
            ChangeNews();
        }

        private void ChangeNews()
        {
            _model.ChangeCurrentNews();
            _view.ChangeNewsSprite(_model.CurrentNews.Sprite);
        }
    }
}
