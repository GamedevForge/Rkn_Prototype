using Project.Common.Core;
using System;
using UnityEngine;

namespace Project.Common.UI
{
    public class NewsController : MonoBehaviour, IQuestEvent<Transform>
    {
        public event Action OnEvent;

        [SerializeField] private NewsIsOverEvent _newsIsOverEvent;

        private NewsModel _model;
        private INewsViewModel _view;
        private NewsApproveOrRejectAnimations _animationController;
        private PlayerState _playerState;

        public Transform MarkerTarget => null;

        private bool IsPossible => _playerState.IsProcessing == false &&
            _playerState.IsSitting &&
            _playerState.InComputer &&
            _playerState.InputOnKeyboard &&
            _model.NewsIsNotOverForToday;

        public void Initialize(
            NewsModel newsModel,
            INewsViewModel newsViewModel,
            NewsApproveOrRejectAnimations newsApproveOrRejectAnimations,
            PlayerState playerState)
        {
            _model = newsModel;
            _view = newsViewModel;
            _animationController = newsApproveOrRejectAnimations;
            _playerState = playerState;

            if (_model.NewsIsNotOverForToday == false)
                _view.NewsIsOver();
            else
                _view.ChangeSprite(_model.CurrentNews.Sprite);
        }

        public async void OnApprove()
        {
            if (IsPossible == false)
                return;
            
            OnEvent?.Invoke();
            await _animationController.PlayApproveAnimationAsync();
            ChangeNews();
        }

        public async void OnReject()
        {
            if (IsPossible == false)
                return;

            OnEvent?.Invoke();
            await _animationController.PlayRejectAnimationAsync();
            ChangeNews();
        }

        private void ChangeNews()
        {
            _model.ChangeCurrentNews();
            _model.ApproveOrRejectNews();
            if (_model.NewsIsNotOverForToday == false)
            {
                _newsIsOverEvent.TriggerEvent();
                _view.NewsIsOver();
            }
            else
                _view.ChangeSprite(_model.CurrentNews.Sprite);
        }
    }
}
