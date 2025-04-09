using UnityEngine;
using Project.Common.UI;

namespace Project.Common.Installers
{
    public struct RequirementsWindowData
    {
        public RectTransform TargetRectTransform;
        public RectTransform CanvasRectTransform;
        public RectTransform DataBaseButtonTransform;
        public BaseWidget DataBaseWidget;
        public CloseWidget DataBaseCloseWidget;
        public TMPro.TMP_Text Text;
        public float Duration;
    }
}