using CompanyAdmin.API.Models;
using CompanyAdmin.API.Services.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Services
{
    public class EmailService : IEmailService
    {
        private readonly ISendGridClient _sendGridClient; 

        public EmailService(ISendGridClient sendGridClient, ICurrentUserService currentUserService)
        {
            _sendGridClient = sendGridClient; 
        }

        public async Task SendEmailAsync(EmailDTO emailDTO)
        {
            EmailAddress from = new EmailAddress
            {
                Email = emailDTO.From.Email,
                Name = emailDTO.From.Name
            };

            List<EmailAddress> tos = emailDTO.Tos.Select(x => new EmailAddress
            {
                Email = x.Email,
                Name = x.Name
            }).ToList();

            var sendGridMessage = new SendGridMessage();
            sendGridMessage.SetFrom(from.Email, from.Name);
            sendGridMessage.AddTos(tos);

            if (emailDTO.Ccs != null)
            {
                List<EmailAddress> ccs = emailDTO.Ccs.Select(x => new EmailAddress
                {
                    Email = x.Email,
                    Name = x.Name
                }).ToList();

                sendGridMessage.AddCcs(ccs);
            }

            sendGridMessage.SetTemplateId(emailDTO.TemplateId);
            sendGridMessage.SetTemplateData(emailDTO.TemplateData);

            var response = await _sendGridClient.SendEmailAsync(sendGridMessage);

            //  if (response.StatusCode != HttpStatusCode.Accepted)

        }
    }
}
