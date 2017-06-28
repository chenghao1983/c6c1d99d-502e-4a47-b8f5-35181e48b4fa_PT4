using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace SendGridSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Execute().Wait();
        }

        static async Task Execute()
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("admin@activelearningpt4.net", "Active Learning Admin");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("joechenghao@gmail.com", "Joe");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            
            //var apiKey = "Use-api-key-here";
            //var client = new SendGridClient(apiKey);

            //List<EmailAddress> tos = new List<EmailAddress>();
            //tos.Add(new EmailAddress("joechenghao@gmail.com", "Joe"));
            //tos.Add(new EmailAddress("smita.bisoyi08@gmail.com", "Smita"));

            //var msg = new SendGridMessage();
            //msg.SetFrom(new EmailAddress("test@example.com", "CA Support Team"));
            //msg.SetSubject("Testing SendGrid!");
            //msg.AddTos(tos);
            //msg.SetTemplateId("use-template-id-here");
            ////substitution to template Dear -NAME-.
            //msg.AddSubstitution("-NAME-", "User");
            //var response = await client.SendEmailAsync(msg);
        }
    }
}
