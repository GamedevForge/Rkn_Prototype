using Cysharp.Threading.Tasks;

namespace Project.Common.UI
{
    public interface IWindowController
    { 
        bool OpenOrCloseInProcessing { get; }
        UniTask OpenWindow();
        UniTask CloseWindow();
    }
}
