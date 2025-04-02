using Cysharp.Threading.Tasks;

namespace Project.Common.UI
{
    public interface IWindowAnimation
    {
        UniTask PlayOpenAnimationAsync();
        UniTask PlayCloseAnimationAsync();
    }
}
