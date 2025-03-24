using UnityEngine;
using System;

namespace Project.Common.Core
{
    public class PlayerRayCasterModel : IObjectChangedEvent<GameObject>
    {
        public event Action<GameObject> OnCurrentObjectChanged;
        public GameObject CurrentGameObject { get; private set; }

        public void ChangeCurrentObject(GameObject gameObject)
        {
            if (gameObject != CurrentGameObject)
            {
                CurrentGameObject = gameObject;
                OnCurrentObjectChanged?.Invoke(gameObject);
            }
            else 
                CurrentGameObject = gameObject;
        }
    }
}


