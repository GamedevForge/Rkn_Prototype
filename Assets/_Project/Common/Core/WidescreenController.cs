using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

namespace Project.Common.Core
{
    public class WidescreenController
    {
        private readonly IProperty<WidescreenAnimation> _animation;

        public WidescreenController(IProperty<WidescreenAnimation> animation) =>
            _animation = animation;

        public async UniTask PlayShowAnimationAsync() => 
            await _animation.Property.ShowImageAsync();

        public async UniTask PlayHideAnimationAsync() => 
            await _animation.Property.HideImageAsync();
    }
}