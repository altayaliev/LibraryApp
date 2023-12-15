using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace LibraryApp.Utility
{
    public class EmailSender : IEmailSender
    {

        public  Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
         //   SmtpClient smtpClient = new SmtpClient
         //   {
         //       Port = 465,
         //       Host = "smtp.gmail.com",
         //       EnableSsl = true,
                
         //       DeliveryMethod = SmtpDeliveryMethod.Network,
         //       UseDefaultCredentials = false,
         //       Credentials = new NetworkCredential("alief.altay@gmail.com", "")
         //   };
           
         //return    smtpClient.SendMailAsync("alief.altay@gmail.com",email,subject,htmlMessage);

            return Task.CompletedTask;
        }
    }
}
