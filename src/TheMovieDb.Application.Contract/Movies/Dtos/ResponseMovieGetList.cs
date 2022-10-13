using Yella.Domain.Dto;

namespace TheMovieDb.Application.Contract.Movies.Dtos;

public class ResponseMovieGetList : FullAuditedEntityDto<Guid>
{

    public bool? Adult { get; set; }

    public string? BackdropPath { get; set; }


    public int? Budget { get; set; }

    public string? Homepage { get; set; }

    public string? ImdbId { get; set; }

    public string OriginalLanguage { get; set; } = string.Empty;

    public string OriginalTitle { get; set; } = string.Empty;

    public string Overview { get; set; } = string.Empty;

    public double Popularity { get; set; }

    public string? PosterPath { get; set; }

    public DateTime ReleaseDate { get; set; }

    public int? Revenue { get; set; }

    public int? Runtime { get; set; }

    public string? Status { get; set; }

    public string? TagLine { get; set; }

    public string Title { get; set; } = string.Empty;

    public bool? Video { get; set; }

    public double VoteAverage { get; set; }

    public int VoteCount { get; set; }

    public int IntegrationId { get; set; }

}