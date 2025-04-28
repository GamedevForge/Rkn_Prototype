namespace Project.Common.Core.SaveLoadSystem
{
    public class SaveLoadModel : ISetCurrentData<PlayerSaveData>, IProperty<PlayerSaveData>
    {        
        public PlayerSaveData Property { get; private set; }

        public void SetCurrentData(PlayerSaveData playerSaveData) =>
            Property = playerSaveData;
    }
}

