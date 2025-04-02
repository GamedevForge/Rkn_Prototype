using UnityEngine;

namespace Project.Common.UI
{
    public class NewsWindowModel : IOpenCloseUI
    {
        public Sprite NewsSprite { get; private set; } 
        public bool IsOpen { get; private set; } = false;

        public void ChangeNewsSprite(Sprite newsSprite) =>
            NewsSprite = newsSprite;

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