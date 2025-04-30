using UnityEngine;

namespace Project.Common.Core
{
    public class PlayerFactory : IFactory<GameObject, GameObject>
    {
        public GameObject Create(GameObject prefab)
        {
            GameObject playerGameObject = GameObject.Instantiate(prefab, null);
            GameObject.DontDestroyOnLoad(playerGameObject);
            return playerGameObject;
        }
    }
}