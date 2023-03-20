using Eduversity.com.Shared.Models.Data;

namespace Eduversity.com.Client.Services.SessionService
{
    public class SessionService : ISessionService
    {
        public List<Session> Sessions
        {
            get
            {
                List<Session> sessions = new();
                int startYear = 2016;
                int endYear = DateTime.Now.Year;

                string session = string.Empty;
                for (int i = endYear; i >= startYear; i--)
                {
                    session = $"{i}/{i + 1}";
                    sessions.Add(new Session { Name = session, Value = session });
                }
                return sessions;
            }
        }
    }
}
