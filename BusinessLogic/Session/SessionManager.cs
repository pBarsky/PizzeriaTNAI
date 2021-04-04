using System;
using System.Web;
using System.Web.SessionState;

namespace BusinessLogic.Session
{
    public class SessionManager : ISessionManager
    {
        private readonly HttpSessionState _session;

        public SessionManager()
        {
            _session = HttpContext.Current.Session;
        }

        public void Abandon()
        {
            _session.Abandon();
        }

        public T Get<T>(string key)
        {
            return (T)_session[key];
        }

        public void Set<T>(string name, T value)
        {
            _session[name] = value;
        }

        public T TryGet<T>(string key)
        {
            try
            {
                return (T)_session[key];
            }
            catch (NullReferenceException)
            {
                return default(T);
            }
        }
    }
}