using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace LokalnyTarg.Common
{
    public static class SendEmail
    {
        public static void SendRegistrationEmail(string mail, string token, string username)
        {
            string subject = "Confirm your account";
            string text = $"Please confirm your account by clicking this link: <a href= \"http://lokalny.targ.pl:8080/register/confirm?Token={token}&userName={username}\">link</a>";
            SendEmailToUser(mail,text,subject);
        }
        public static void SendResetPasswordEmail(string mail, string token, string username)
        {
            string subject = "ResetPassword";
            string text = $"If you want to reset your password click <a href= \"http://lokalny.targ.pl:8080/login/reset/confirm?Token={token}&userName={username}\">link</a>";
            SendEmailToUser(mail, text, subject);
        }

        private static async void SendEmailToUser(string mail,string text,string subject)
        {
            MailAddress to = new MailAddress(mail);
            MailAddress addressFrom = new MailAddress("lokalnytarg@gmail.com", "Lokalny Targ");
            MailMessage message = new MailMessage(addressFrom, to)
            {
                Subject = subject,
                Body = text,
                IsBodyHtml = true
            };

            SmtpClient client = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("lokalnytarg@gmail.com", "KaPaPaPaPa1"),
                EnableSsl = true
            };

            try
            {
                await client.SendMailAsync(message);
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
