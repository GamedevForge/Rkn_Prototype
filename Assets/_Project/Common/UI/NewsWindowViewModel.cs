using UnityEngine;
using UnityEngine.UI;

namespace Project.Common.UI
{
    public class NewsWindowViewModel : BaseWindowViewModel, INewsViewModel
    {
        private readonly Image _newsImage;
        private readonly GameObject _newsGameObject;
        private readonly GameObject _windowIfNewsIsOver;

        public Sprite NewsSprite { get; private set; } 

        public NewsWindowViewModel(
            Image newsImage, 
            GameObject newsGameObject, 
            GameObject windowIfNewsIfOver)
        {
            _newsImage = newsImage;
            _newsGameObject = newsGameObject;
            _windowIfNewsIsOver = windowIfNewsIfOver;
        }

        public void ChangeSprite(Sprite sprite)
        {
            NewsSprite = sprite;
            _newsImage.sprite = sprite;
        }

        public void NewsIsOver()
        {
            _newsGameObject.SetActive(false);
            _windowIfNewsIsOver.SetActive(true);
        }
    }
}