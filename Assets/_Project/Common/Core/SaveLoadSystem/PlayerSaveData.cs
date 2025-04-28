using UnityEngine;

namespace Project.Common.Core.SaveLoadSystem
{
    public class PlayerSaveData
    {
        public string SceneName;
        public Vector3 PlayerWorldPosition;
        public int Day;
        public QuestSaveData QuestSaveData;
    }
}

