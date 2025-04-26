using Cursive.Web.MVC.Services.Interfaces;
using FluentEmail.Core;
using FluentEmail.Core.Models;

namespace Cursive.Web.MVC.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly string _from;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _from = _configuration["FluentEmail:From"] ?? string.Empty;
        }

        public async Task<SendResponse> SendEmail(string to, string content)
        {
            return await Email.From(_from).To(to).Body(content).SendAsync();
        }
    }
}
