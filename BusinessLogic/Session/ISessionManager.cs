namespace BusinessLogic.Session
{
    public interface ISessionManager
    {
        void Abandon();

        T Get<T>(string key);

        void Set<T>(string name, T value);

        T TryGet<T>(string key);
    }
}