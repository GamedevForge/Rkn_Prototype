using TMPro;
using UnityEngine;

namespace Project.Common.UI
{ 
    [RequireComponent (typeof(TMP_Text))]
    public class TextView : MonoBehaviour
    {
        private TMP_Text Text => GetComponent<TMP_Text>();

        public void DrawText(string text) =>
            Text.text = text;
    }
}
