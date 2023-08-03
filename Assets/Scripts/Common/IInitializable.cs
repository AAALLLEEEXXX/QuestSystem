namespace Common
{
    public interface IInitializable<in T> : ICustomDisposable
    {
        void Init(T inputParams);
    }
}
