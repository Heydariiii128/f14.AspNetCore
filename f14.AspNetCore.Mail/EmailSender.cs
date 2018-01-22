using System.Threading.Tasks;

namespace f14.AspNetCore.Mail
{
    public class EmailSender : IEmailSender
    {
        private string _server;
        private int _port;
        private bool _useSsl;
        private string _login;
        private string _password;
        private string _fromName;
        private string _fromEmail;

        public EmailSender(string server, int port, bool useSsl, string login, string password, string fromName, string fromEmail)
        {
            ExHelper.NotNullString(() => server);
            ExHelper.NotNullString(() => login);
            ExHelper.NotNullString(() => password);
            ExHelper.NotNullString(() => fromEmail);            

            _server = server;
            _port = port;
            _useSsl = useSsl;
            _login = login;
            _password = password;
            _fromName = fromName;
            _fromEmail = fromEmail;
        }

        public Task SendAsync(string toEmail, string subject, string message)
        {
            ExHelper.NotNullString(() => toEmail);
            ExHelper.NotNullString(() => subject);
            ExHelper.NotNullString(() => message);
            return MessageService.SendEmailAsync(_server, _port, _useSsl, _login, _password, _fromName, _fromEmail, "", toEmail, subject, message);
        }
    }
}
