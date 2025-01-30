using Cursive.Communication.Dtos.Interfaces;
using Cursive.Communication.Dtos.User.Responses;

namespace Cursive.Web.HttpCore.Interfaces;

public interface IReCaptchaClient
{
    Task<IResponseDto<ReCaptchaResponse?>?> ValidateReCaptchaAsync(string recaptchaToken);
}
