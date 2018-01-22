using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace f14.AspNetCore.Mail
{
    public interface IEmailSender
    {
        Task SendAsync(string toEmail, string subject, string message);
    }
}
