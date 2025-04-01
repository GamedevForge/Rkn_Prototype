using UnityEngine;

namespace Project.Common.UI
{
    public class CanvasZeroScale : MonoBehaviour
    {
        private void Awake() =>
            transform.localScale = new Vector3(
                transform.localScale.x, 
                0f, 
                transform.localScale.z);
    }
}
