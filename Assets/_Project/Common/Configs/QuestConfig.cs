using Project.Common.Core.Quest;
using Sirenix.OdinInspector;
using System;

namespace Project.Common.Configs
{
    [Serializable]
    public class QuestConfig
    {     
        public string ID;
        public string QuestDescription;
        public QuestType Type;

        [ReadOnly] public bool IsActive = true; 
    }
}
