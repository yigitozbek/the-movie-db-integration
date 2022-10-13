using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Configuration;
using TheMovieDb.Domain.Shared.Mails.Smtp;
using TheMovieDb.Domain.Shared.Mails.Smtp.Dtos;
using Yella.Utilities.Results;

namespace TheMovieDb.Domain.Shared.Mails;

public class SmtpClientApplicationService : IMailService
{
    private readonly IConfiguration _configuration;

    public SmtpClientApplicationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Base mail gönderme methodu
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    private IResult BaseSendMail(RequestSendMail model)
    {
        var username = _configuration["SmtpClient:Username"];
        var password = _configuration["SmtpClient:Password"];
        var mailAddress = _configuration["SmtpClient:MailAddress"];
        var displayName = _configuration["SmtpClient:DisplayName"];
        var timeOut = _configuration["SmtpClient:TimeOut"];
        var host = _configuration["SmtpClient:Host"];
        var port = Convert.ToInt32(_configuration["SmtpClient:Port"]);

        SmtpClient smtp = new(host, port);

        smtp.Timeout = Convert.ToInt32(timeOut);

        smtp.Credentials = new NetworkCredential(username, password);

        MailMessage mailMsg = new();

        mailMsg.Headers.Add("X-MC-Track", "opens,click_all");
        mailMsg.From = new MailAddress(mailAddress, displayName);
        mailMsg.IsBodyHtml = true;
        mailMsg.Body = model.Body;
        mailMsg.BodyEncoding = Encoding.UTF8;
        mailMsg.Subject = model.Subject;

        foreach (var item in model.ToEMails)
        {
            mailMsg.To.Add(item);
        }

        smtp.Send(mailMsg);

        return new SuccessResult("Success");
    }

    /// <summary>
    /// Belirtilen e-mail adreslerine mail gönderir.
    /// </summary>
    /// <param name="requestMail"></param>
    /// <returns></returns>
    public IResult SendMail(RequestSendMail requestMail)
    {
        var result = BaseSendMail(new(body: requestMail.Body, subject: requestMail.Subject, toEMails: requestMail.ToEMails));
        return result;
    }

}
