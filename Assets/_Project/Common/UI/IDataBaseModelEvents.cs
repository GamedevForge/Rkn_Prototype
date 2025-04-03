using System.Collections.Generic;

namespace Project.Common.UI
{
    public interface IDataBaseModelEvents
    {
        event System.Action<string> OnRequestChanged;
        event System.Action<Dictionary<string, string>> OnChangeFindResults;
    }
}
