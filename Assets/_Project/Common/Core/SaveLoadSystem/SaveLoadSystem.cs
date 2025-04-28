using System.IO;
using UnityEngine;

namespace Project.Common.Core.SaveLoadSystem
{
    public class SaveLoadSystem
    {
        private string FilePath => Application.persistentDataPath + "/PlayerData.json";
        
        public void Save(PlayerSaveData playerSaveData)
        {
            string playerSaveDataJson = JsonUtility.ToJson(playerSaveData);
            File.WriteAllText(FilePath, playerSaveDataJson);
        }

        public PlayerSaveData Load()
        {
            string playerSaveDataJson = File.ReadAllText(FilePath);
            return JsonUtility.FromJson<PlayerSaveData>(playerSaveDataJson);
        }
    }
}