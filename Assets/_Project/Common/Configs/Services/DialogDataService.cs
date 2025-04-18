namespace Project.Common.Configs
{
    public class DialogDataService
    {
        private readonly DialogListData _dialogDataList;

        public DialogDataService(DialogListData dialogDataList) =>
            _dialogDataList = dialogDataList;

        public DialogData GetDialogData(string id)
        {
            foreach (DialogData dialogData in _dialogDataList.DialogsData)
            {
                if (id == dialogData.NPCID)
                    return dialogData;
            }
            return null;
        }
    }
}
