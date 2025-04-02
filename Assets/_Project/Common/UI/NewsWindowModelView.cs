using UnityEngine;

namespace Project.Common.UI
{
    public class NewsWindowModelView : IOpenCloseUI, IWindowWithSprite
    {
        public Sprite NewsSprite { get; private set; } 
        public bool IsOpen { get; private set; } = false;

        public void ChangeNewsSprite(Sprite sprite) =>
            NewsSprite = sprite;


        public void Open()
        {
            IsOpen = true;
        }

        public void Close()
        {
            IsOpen = false;
        }
    }
}