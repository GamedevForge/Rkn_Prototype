using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Common.UI
{
    public class CanvasRepository
    {
        private readonly List<GameObject> _canvasList = new();

        public void Add(GameObject gameObject) =>
            _canvasList.Add(gameObject);

        public void Remove(GameObject gameObject) =>
            _canvasList.Remove(gameObject);

        public void SetStateAllCanvasWithout(bool state, params GameObject[] gameObjects)
        {
            foreach(var canvas in _canvasList)
            {
                if (gameObjects.Contains(canvas))
                    continue;
                else
                    canvas.gameObject.SetActive(state);
            }
        }
    }
}