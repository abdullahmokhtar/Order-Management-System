using System.Net;
using System.Net.Mail;

namespace RouteSummitTask.PL.Helper
{
    public static class EmailSettigns
    {
        public static void SendEmail(Email email)
        {
            SmtpClient client = new("smtp.gmail.com", 587);

            client.EnableSsl = true;

            client.Credentials = new NetworkCredential("abdullahmokhtr55@gmail.com", "rhqsgtbocklxpllx");

            client.Send("abdullahmokhtr55@gmail.com", email.To, email.Title, email.Body);
        }
    }
}
