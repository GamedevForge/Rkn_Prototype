using System;
using UnityEngine;

namespace Project.Common.Configs
{
    [Serializable]
    public class AnswerConfig
    {
        [field: SerializeField] public string AnswerText { get; private set; }
        [field: SerializeField] public DialogData DialogData { get; private set; }
    }
}
