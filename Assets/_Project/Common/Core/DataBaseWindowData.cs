using UnityEngine;
using Project.Common.UI;
using Project.Common.Configs;
using TMPro;

namespace Project.Common.Core
{
    public struct DataBaseWindowData
    {
        public RectTransform TargetRectTransform;
        public RectTransform CanvasRectTransform;
        public RectTransform DataBaseButtonTransform;
        public BaseWidget DataBaseWidget;
        public CloseWidget DataBaseCloseWidget;
        public InquiriesData InquiriesData;
        public TMP_Text TextInfo;
        public GameObject ButtonPrefab;
        public Transform ButtonPrefabsParent;
        public Transform SecondParent;
        public DataBaseController DataBaseController;
        public float Duration;
    }
}


