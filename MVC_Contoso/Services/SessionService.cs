
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using MVC_Contoso.Models;
using System.Text.Json;

namespace MVC_Contoso.Services
{
    public interface ISessionService
    {
        void Initialize();

        Sessions GetSessions();

        Session GetSession(int sessionId);

        bool ReserveSeat(int roomId);
    }

    public class SessionService : ISessionService
    {
        private Sessions _sessions = null;


        public SessionService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }
        public IWebHostEnvironment WebHostEnvironment { get; }

        private string SessionsFile
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "Data", "Sessions.json"); }
        }

        public IEnumerable<Session> InitializedSessions()
        {
            using var jsonFileReader = File.OpenText(SessionsFile);
            return JsonSerializer.Deserialize<List<Session>>(jsonFileReader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        public void Initialize()
        {
            _sessions = new Sessions()
            {
                sessionsList = new List<Session>()
            };
            foreach (var item in InitializedSessions())
            {
                _sessions.sessionsList.Add(item);
                Console.WriteLine(item);
            }
            
        }

        public Sessions GetSessions()
        {
            return _sessions;
        }

        public Session GetSession(int roomId)
        {
            Session session = _sessions.sessionsList[roomId - 1];
            return session;
        }

        public bool ReserveSeat(int roomId)
        {
            if (_sessions.sessionsList[roomId - 1].SeatsAvailable > 0)
            {
                _sessions.sessionsList[roomId - 1].SeatsAvailable -= 1;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
