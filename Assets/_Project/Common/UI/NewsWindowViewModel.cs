using UnityEngine;
using UnityEngine.UI;

namespace Project.Common.UI
{
    public class NewsWindowViewModel : BaseWindowViewModel, IWindowWithSprite
    {
        private readonly Image _newsImage;
        
        public Sprite NewsSprite { get; private set; } 

        public NewsWindowViewModel(Image newsImage)
        {
            _newsImage = newsImage;
        }

        public void ChangeNewsSprite(Sprite sprite)
        {
            NewsSprite = sprite;
            _newsImage.sprite = sprite;
        }
    }
}