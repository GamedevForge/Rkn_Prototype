using Project.Common.Configs;
using System.Collections.Generic;

namespace Project.Common.UI
{
    public class DataBaseModel : IDataBaseModelEvents
    {
        public event System.Action<string> OnRequestChanged;
        public event System.Action<Dictionary<string, string>> OnChangeFindResults;
        
        private readonly InquiriesData _data;
        
        private Dictionary<string, string> _findResults = new();

        public string CurrentRequest { get; private set; } = string.Empty;
        public IEnumerable<KeyValuePair<string, string>> FindResults => _findResults;
        public IEnumerable<KeyValuePair<string, string>> Inquiries => _data.Inquiries;

        public DataBaseModel(InquiriesData data) =>
            _data = data;

        public void ChangeValue(string request)
        {
            CurrentRequest = request;
            OnRequestChanged?.Invoke(CurrentRequest);
        }

        public void ChangeFindResults(Dictionary<string, string> results)
        {
            _findResults = results;
            OnChangeFindResults?.Invoke(results);
        }
    }
}
