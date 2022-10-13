using TheMovieDb.Application.Contract.Movies.Dtos;
using TheMovieDb.Domain.Shared.Movies;
using Yella.Utilities.Results;

namespace TheMovieDb.Application.Contract.Movies;

public interface IMovieService
{
    Task<IDataResult<PageResultDto<ResponseMovieGetList>>> GetListAsync(RequestMovieGetList input);
    Task<IDataResult<ResponseMovieGetById?>> GetByIdAsync(RequestMovieGetById input);
    Task<IResult> RecommendSelectedMovieAsync(RequestMovieRecommendSelectedMovie input);
    Task<IResult> RateMovieAsync(RequestMovieRateMovie input);
}