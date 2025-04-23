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
        private readonly SignalBus _signalBus;

        private int CurrentTakeCount = 0;

        public DialogController(
            DialogModel dialogModel, 
            DialogViewController viewController,
            SignalBus signalBus)
        {
            _dialogModel = dialogModel;
            _viewController = viewController;
            _signalBus = signalBus;
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
            if (_dialogModel.CurrentTakesData[0].WhoSpeaks == WhoSpeaks.Player)
                await _viewController.ShowDialogBoard(_dialogModel.PlayerName);
            else
                await _viewController.ShowDialogBoard(_dialogModel.NPCName);
            
            GoToNextTakeOrStopDialog();
        }

        public async UniTask StopDialog()
        {
            await _viewController.HideDialogBoard();
            _dialogModel.SetDialogState(false);
            CurrentTakeCount = 0;
        }

        private async void GoToNextTakeOrStopDialog(TakeData[] takesData = null)
        {
            if (CurrentTakeCount >= _dialogModel.CurrentTakesData.Count)
            {
                await StopDialog();
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
            _viewController.HideGoToNextTakeButton();

            _signalBus.Fire(new DialogSignal { NPCID = _dialogModel.CurrentDialogData.NPCID, SignalID = takeData.SignalID });

            if (takeData.WhoSpeaks == WhoSpeaks.Player)
            {
                await _viewController.ShowNextUIElement(takeData, _dialogModel.PlayerName);
                if (takeData.Type == TakeType.OrdinaryTake)
                    _viewController.ShowGoToNextTakeButton();
            }
            else
            {
                await _viewController.ShowNextUIElement(takeData, _dialogModel.NPCName);
                if (takeData.Type == TakeType.OrdinaryTake)
                    _viewController.ShowGoToNextTakeButton();
            }

            if (takeData.Type == TakeType.Optionally)
                _viewController.HideGoToNextTakeButton();
        }
    }
}