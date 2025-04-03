using System;
using System.Collections.Generic;
using TMPro;
using Zenject;

namespace Project.Common.UI
{
    public class DataBaseWindowViewModel : BaseWindowViewModel, IInitializable, IDisposable
    {
        public event Action<Dictionary<string, string>> OnFindResult;
        
        private readonly IDataBaseModelEvents _events;
        private readonly TMP_Text _info;

        public DataBaseWindowViewModel(IDataBaseModelEvents events, TMP_Text info)
        {
            _events = events;
            _info = info;
        }

        public void Initialize() =>
            _events.OnChangeFindResults += SendResult;
        
        public void Dispose() =>
            _events.OnChangeFindResults -= SendResult;

        public void SendResult(Dictionary<string, string> results) =>
            OnFindResult?.Invoke(results);

        public void ChangeTextInfo(string info) =>
            _info.text = info;
    }
}