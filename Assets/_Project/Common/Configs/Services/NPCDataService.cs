namespace Project.Common.Configs
{
    public class NPCDataService
    {
        private readonly NPCDataList _npcDataList;

        public NPCDataService(NPCDataList npcDataList) =>
            _npcDataList = npcDataList;

        public NPCData GetNPCData(string id) 
        {
            foreach (NPCData npcData in _npcDataList.NPCSData)
            {
                if (id == npcData.ID)
                    return npcData;
            }
            return null;
        }
    }
}
