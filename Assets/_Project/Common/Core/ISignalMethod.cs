namespace Project.Common.Core
{
    public interface ISignalMethod<TData>
    {
        void TriggerMethod(TData data);
    }
}