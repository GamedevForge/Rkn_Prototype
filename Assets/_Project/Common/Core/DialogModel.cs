using Project.Common.Configs;
using System;

namespace Project.Common.Core
{
    public class DialogModel
    {
        public event Action<bool> OnDialogStateChange;
        
        public bool DialogIsProcessing { get; private set; } = false;
        public bool DialogIsPossible => CurrentDialogData != null;
        public DialogData CurrentDialogData { get; private set; }
        public System.Collections.Generic.List<TakeData> CurrentTakesData { get; private set; } = new();
        public string NPCName { get; private set; }
        public string PlayerName => _playerData.PlayerName;

        private readonly DialogDataService _dialogDataService;
        private readonly NPCDataService _npcDataService;
        private readonly IPlayerName _playerData;

        public DialogModel(
            DialogDataService dialogDataService,
            NPCDataService npcDataService,
            IPlayerName playerName)
        {
            _dialogDataService = dialogDataService;
            _npcDataService = npcDataService;
            _playerData = playerName;
        }

        public void SetDialogState(bool state)
        {
            DialogIsProcessing = state;
            OnDialogStateChange?.Invoke(state);
        }

        public void SetCurrentDialogDataAndName(string id)
        {
            CurrentTakesData.Clear();
            CurrentDialogData = _dialogDataService.GetDialogData(id);
            NPCName = _npcDataService.GetNPCData(id).Name;
            CurrentTakesData.AddRange(CurrentDialogData.DialogTakes);
        }
    }
}