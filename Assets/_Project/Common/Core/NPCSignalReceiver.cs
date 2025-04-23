using UnityEngine;

namespace Project.Common.Core
{
    public class NPCSignalReceiver : BaseSignalReceiver<DialogSignal>
    {
        [SerializeField] private string NPCID;

        public override void SignalMethod(DialogSignal signal)
        {
            if (signal.SignalID == "none")
                return;
            
            if (signal.NPCID == NPCID)
                Signal.TriggerMethod(signal);
        }
    }
}