using UnityEngine;

namespace Project.Common.Core
{
    public class NPCSignalReceiver : BaseSignalReceiver<NPCData>
    {
        [SerializeField] private string NPCID;

        public override void SignalMethod(NPCData data)
        {
            if (data.NPCID == NPCID)
                Signal.TriggerMethod(data);
        }
    }
}