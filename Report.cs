using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FreezeTest
{
    class Report
    {
        public static void SendEmail(string crashmessage)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("narek.hayrapetyan@efusoft.com", "Nar240893");
            MailAddress from = new MailAddress("narek.hayrapetyan@efusoft.com",
            "Narek " + " Hayrapetyan",
            System.Text.Encoding.UTF8);
            MailAddress to = new MailAddress("erik.asatryan@efusoft.com");
            MailMessage message = new MailMessage(from, to);
            message.Body = crashmessage;
            message.Subject = "Crash";
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            // string userState = "test message1";
            client.SendMailAsync(message).GetAwaiter().GetResult();
        }
    }
}
