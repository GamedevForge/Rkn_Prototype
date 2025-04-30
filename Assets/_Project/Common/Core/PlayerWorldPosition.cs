using Project.Common.Core.SaveLoadSystem;
using System;
using UnityEngine;

namespace Project.Common.Core
{
    public class PlayerWorldPosition
    {
        private readonly IProperty<PlayerSaveData> _saveLoadModel;
        private readonly ISaveController _saveController;

        public PlayerWorldPosition(
            IProperty<PlayerSaveData> saveLoadModel, 
            ISaveController saveController)
        {
            _saveLoadModel = saveLoadModel;
            _saveController = saveController;
        }

        public void SavePosition(Vector3 position)
        {
            _saveLoadModel.Property.PlayerWorldPosition = position;
            _saveController.Save();
        }
    }
}