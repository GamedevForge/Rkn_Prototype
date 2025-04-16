using Cysharp.Threading.Tasks;

namespace Project.Common.UI
{
    public interface IOpenCloseAnimation
    {
        UniTask PlayOpenAnimationAsync();
        UniTask PlayCloseAnimationAsync();
    }
}
