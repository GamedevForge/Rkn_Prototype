using System.Collections.Generic;
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

        public void SetStateAllCanvasWithout(GameObject gameObject, bool state)
        {
            foreach(var canvas in _canvasList)
            {
                if (canvas != gameObject)
                    canvas.gameObject.SetActive(state);
            }
        }
    }
}