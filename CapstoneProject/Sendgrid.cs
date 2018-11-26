using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject
{
    public static class Sendgrid
    {
        // The email this sends only shows 'World!' (no 'Hello')
        //public static async Task Execute()
        //{
        //    var apiKey = ApiKeys.sendgridKey;
        //    var client = new SendGridClient(apiKey);
        //    var from = new EmailAddress("test@example.com", "Example User");
        //    var subject = "Sending with SendGrid is Fun";
        //    var to = new EmailAddress("esoemad5@gmail.com", "Elliott");
        //    var plainTextContent = "Hello";
        //    var htmlContent = "<strong>World!</strong>";
        //    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        //    var response = await client.SendEmailAsync(msg);
        //}

        public static async Task SendMail(string recipient, string subject, string body)
        {
            var apiKey = ApiKeys.SendGridKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("WeeklyVeganFood@PureVegan.com", "The PureVegan Team");
            var to = new EmailAddress(recipient, null);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, body, null);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
