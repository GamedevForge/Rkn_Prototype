using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Common.Configs
{
    [CreateAssetMenu(fileName = "TakeData", menuName = "Project/TakeData")]
    public class TakeData : SerializedScriptableObject
    {
        [field: SerializeField] public WhoSpeaks WhoSpeaks { get; private set; }
        [field: SerializeField] public TakeType Type { get; private set; }
        [field: SerializeField] public string Text { get; private set; }
        [field: SerializeField] public string SignalID { get; private set; } = "none";

        [field: SerializeField] public Dictionary<string, TakeData[]> DialogOptionsAfterPlayersAnswer { get; private set; }
    }

    public enum WhoSpeaks
    {
        NPC,
        Player,
    }
}
