using System;
using UnityEngine;

namespace Project.Common.Configs
{
    [Serializable]
    public class TakeConfig
    {
        [field: SerializeField] public WhoSpeaks WhoSpeaks { get; private set; }
        [field: SerializeField] public TakeType Type { get; private set; }
        [field: SerializeField] public string Text { get; private set; }
        [field: SerializeField] public string SignalID { get; private set; } = "none";

        [field: SerializeField] public AnswerConfig[] DialogOptionsAfterPlayersAnswer { get; private set; }
    }
}
