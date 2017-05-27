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
        }
    }
}
