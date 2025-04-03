using System.Collections.Generic;

namespace Project.Common.UI
{
    public class SearchEngineBase
    {
        private readonly Dictionary<string, string> _dataBase;

        public SearchEngineBase(Dictionary<string, string>  dataBase) =>
            _dataBase = dataBase;

        public Dictionary<string, string> Search(string request)
        {
            Dictionary<string, string> results = new();
            
            foreach (string key in _dataBase.Keys)
            {
                if (key.ToLower().Contains(request.ToLower()))
                    results.Add(key, _dataBase[key]);
            }

            return results;
        }
    }
}
