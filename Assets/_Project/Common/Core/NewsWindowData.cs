using UnityEngine;
using Project.Common.UI;
using Project.Common.Configs;

namespace Project.Common.Core
{
    public struct NewsWindowData
    {
        public NewsListData News;
        public BaseWidget NewsWidget;
        public RectTransform CanvasRectTransform;
        public RectTransform TargetRectTransform;
        public RectTransform NewsButtonTransform;
        public NewsController NewsController;
        public Transform RejectButtonTransform;
        public Transform ApproveButtonTransform;
        public Transform CameraLookAtPointTransform;
        public Transform ArmTransform;
        public ApproveOrRejectData ApproveOrRejectData;
        public float Duration;
    }
}


