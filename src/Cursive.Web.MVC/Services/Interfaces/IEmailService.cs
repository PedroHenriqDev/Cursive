using FluentEmail.Core.Models;

namespace Cursive.Web.MVC.Services.Interfaces;

public interface IEmailService
{
    Task<SendResponse> SendEmail(string to, string content);
}
