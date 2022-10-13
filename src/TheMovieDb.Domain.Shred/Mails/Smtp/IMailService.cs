using TheMovieDb.Domain.Shared.Mails.Smtp.Dtos;
using Yella.Utilities.Results;

namespace TheMovieDb.Domain.Shared.Mails.Smtp;

public interface IMailService
{
    /// <summary>
    /// Belirtilen e-mail adreslerine mail gönderir.
    /// </summary>
    /// <param name="requestMail"></param>
    /// <returns></returns>
    IResult SendMail(RequestSendMail requestMail);
}