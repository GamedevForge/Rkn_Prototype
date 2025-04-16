using Project.Common.Core.Quest;
using System;

namespace Project.Common.Configs
{
    [Serializable]
    public class QuestConfig
    {     
        public string ID;
        public string QuestDescription;
        public QuestType Type;
        public bool IsActive = true; 
    }
}
