using Cysharp.Threading.Tasks;

namespace Project.Common.UI
{
    public interface IWindowController
    { 
        UniTask OpenWindow();

        UniTask CloseWindow();
    }
}
