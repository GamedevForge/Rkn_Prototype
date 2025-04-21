using Zenject;
using Project.Common.Configs;
using Cysharp.Threading.Tasks;
using System;

namespace Project.Common.Core
{
    public class DialogController : IInitializable, IDisposable
    {
        private readonly DialogModel _dialogModel;
        private readonly DialogViewController _viewController;

        private int CurrentTakeCount = 0;

        public DialogController(
            DialogModel dialogModel, 
            DialogViewController viewController)
        {
            _dialogModel = dialogModel;
            _viewController = viewController;
        }
        
        public void Initialize() =>
            _viewController.OnOptionallyButtonClicked += GoToNextTakeOrStopDialog;
        
        public void Dispose() =>
            _viewController.OnOptionallyButtonClicked -= GoToNextTakeOrStopDialog;

        public async UniTask StartDialog(string id)
        {
            _dialogModel.SetCurrentDialogDataAndName(id);
            _dialogModel.SetDialogState(true);

            if (_dialogModel.DialogIsProcessing == false)
            {
                _dialogModel.SetDialogState(false);
                return;
            }

            _dialogModel.SetDialogState(true);
            await _viewController.ShowDialogBoard();
            GoToNextTakeOrStopDialog();
        }

        public void StopDialog()
        {
            _dialogModel.SetDialogState(false);
            CurrentTakeCount = 0;
        }

        private async void GoToNextTakeOrStopDialog(TakeData[] takesData = null)
        {
            if (CurrentTakeCount >= _dialogModel.CurrentTakesData.Count)
            {
                StopDialog();
                return;
            }

            if (takesData != null)
            {
                _dialogModel.CurrentTakesData.InsertRange(CurrentTakeCount, takesData);
            }

            TakeData takeData = _dialogModel.CurrentTakesData[CurrentTakeCount];

            await GoToNextTake(takeData);

            CurrentTakeCount++;
        }

        private async UniTask GoToNextTake(TakeData takeData)
        {
            if (takeData.WhoSpeaks == WhoSpeaks.Player)
                await _viewController.ShowNextUIElement(takeData, _dialogModel.PlayerName);
            else
                await _viewController.ShowNextUIElement(takeData, _dialogModel.NPCName);

            if (takeData.Type == TakeType.Optionally)
                _viewController.HideGoToNextTakeButton();
            else
                _viewController.ShowGoToNextTakeButton();
        }
    }
}