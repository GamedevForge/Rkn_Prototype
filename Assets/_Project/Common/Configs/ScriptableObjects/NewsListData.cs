using UnityEngine;
using Sirenix.OdinInspector;

namespace Project.Common.Configs
{
    [CreateAssetMenu(fileName = "NewsData", menuName = "Project/NewsData")]
    public class NewsListData : SerializedScriptableObject
    {
        [field: SerializeField] public int NewsPerDay { get; private set; }
        
        [Header("News array:")]
        [field: SerializeField] public NewsConfig[] NewsConfigs { get; private set; }
    }
}
