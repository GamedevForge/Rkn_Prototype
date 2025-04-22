namespace Project.Common.Core
{
    public interface IFactory<TTarget, TData>
    {
        TTarget Create(TData data);
    }
}