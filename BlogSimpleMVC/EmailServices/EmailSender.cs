﻿using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace BlogSimpleMVC.EmailServices
{
    public class EmailSender : IEmailSender
    {
        public AuthMessageSenderOptions Options { get; } // set only via Secret Manager
        public IConfiguration Configuration { get; set; }

        public EmailSender(
            IOptions<AuthMessageSenderOptions> optionsAccessor,
            IConfiguration configuration)
        {
            Options = optionsAccessor.Value;
            this.Configuration = configuration;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Configuration["SendGridKey"], subject, message, email);

        }
        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("prab.dhaliwal95@gmail.com", "Prabdeep Dhaliwal"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}