using UnityEngine;
using Project.Common.UI;
using Project.Common.Configs;
using UnityEngine.UI;

namespace Project.Common.Core
{
    public struct NewsWindowData
    {
        public NewsListData News;
        public BaseWidget NewsWidget;
        public CloseWidget NewsCloseWidget;
        public RectTransform CanvasRectTransform;
        public RectTransform TargetRectTransform;
        public RectTransform NewsButtonTransform;
        public Transform RejectButtonTransform;
        public Transform ApproveButtonTransform;
        public Transform CameraLookAtPointTransform;
        public Transform ArmTransform;
        public ApproveOrRejectData ApproveOrRejectData;
        public Image NewsImage;
        public GameObject NewsGameObject;
        public GameObject ScreenIfNewsIsOver;
        public float Duration;
    }
}


