using UnityEngine;

namespace Project.Common.Core
{
    public class DialogSignalsReceiverList : MonoBehaviour
    {
        [SerializeField] private NPCSignalReceiver[] _receiver;

        public void TriggerAllMethods(DialogSignal dialogSignal)
        {
            foreach (NPCSignalReceiver receiver in _receiver)
                receiver.SignalMethod(dialogSignal);
        }
    }
}