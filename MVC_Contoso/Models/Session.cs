namespace MVC_Contoso.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public string SessionName { get; set; }
        public string Speaker { get; set; }
        public int SeatsAvailable { get; set; }

        public Session()
        {

        }

        public Session(int sessioniD, string sessionName, string speaker, int availSeats)
        {
            this.SessionId = sessioniD;
            this.SessionName = sessionName;
            this.Speaker = speaker;
            this.SeatsAvailable = availSeats;
        }
  
    }
}