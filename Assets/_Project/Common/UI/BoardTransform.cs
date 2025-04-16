using UnityEngine;

namespace Project.Common.UI
{
    public class BoardTransform : MonoBehaviour
    {
        public Transform Parent => GetComponent<Transform>();
    }
}