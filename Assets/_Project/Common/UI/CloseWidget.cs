using UnityEngine.EventSystems;

namespace Project.Common.UI
{
    public class CloseWidget : BaseWidget
    {
        public override void OnPointerClick(PointerEventData eventData)
        {
            if (WindowController.OpenOrCloseInProcessing)
                return;

            WindowController.CloseWindow();
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
        }
    }
}