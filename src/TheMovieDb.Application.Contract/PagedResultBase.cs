using System.ComponentModel.DataAnnotations;
using Yella.Domain.Dto;

namespace TheMovieDb.Application.Contract;

public class PagedResultBase : EntityDto
{
    [Required]
    public short CurrentPage { get; set; } = 1;

    [Required]
    public short PageSize { get; set; } = 10;
}