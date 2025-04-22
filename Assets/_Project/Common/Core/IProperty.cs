namespace Project.Common.Core
{
    public interface IProperty<T>
    {
        T Property { get; }
    }

    public interface IProperty<T1, T2> : IProperty<T1>
    {
        T2 Property1 { get; }
    }
}