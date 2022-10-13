using System.ComponentModel.DataAnnotations;
using Yella.Domain.Dto;

namespace TheMovieDb.Application.Contract.Movies.Dtos;

public class RequestMovieGetById : EntityDto
{
    /// <summary>
    /// Foo
    /// </summary>
    [Required]
    public Guid Id { get; set; }
}