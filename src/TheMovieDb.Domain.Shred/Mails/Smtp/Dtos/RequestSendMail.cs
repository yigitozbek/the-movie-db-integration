using System.ComponentModel.DataAnnotations;

namespace TheMovieDb.Domain.Shared.Mails.Smtp.Dtos;

public class RequestSendMail
{
    public RequestSendMail(string body, string subject, List<string> toEMails)
    {
        Body = body;
        Subject = subject;
        ToEMails = toEMails;
    }

    [Required]
    public string Body { get; set; }
    [Required]
    public string Subject { get; set; }

    [Required]
    public List<string> ToEMails { get; set; }
}