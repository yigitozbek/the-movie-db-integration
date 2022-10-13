using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using TheMovieDb.Application.Contract;
using TheMovieDb.Application.Contract.Identities.Validators;
using TheMovieDb.Application.Contract.Movies;
using TheMovieDb.Application.Contract.Movies.Dtos;
using TheMovieDb.Application.Contract.Movies.Validators;
using TheMovieDb.Application.Helpers;
using TheMovieDb.Domain.Modules.Movies.Managers;
using TheMovieDb.Domain.Shared.Mails.Smtp;
using TheMovieDb.Domain.Shared.Movies;
using Yella.Aspect.PostSharp.Authorizations;
using Yella.Aspect.PostSharp.Validations;
using Yella.AutoMapper.Extensions;
using Yella.EntityFrameworkCore;
using Yella.EntityFrameworkCore.Extensions;
using Yella.Utilities.Results;

namespace TheMovieDb.Application.Movies;

public class MovieApplicationService : IMovieService
{
    private readonly IMailService _mailService;
    private readonly MovieManager _movieManager;
    private readonly IRepository<Domain.Modules.Movies.Movie, Guid> _movieRepository;

    public MovieApplicationService(MovieManager movieManager, IRepository<Domain.Modules.Movies.Movie, Guid> movieRepository, IMailService mailService)
    {
        _movieManager = movieManager;
        _movieRepository = movieRepository;
        _mailService = mailService;
    }


    [AuthorizationAspect(TheMovieDbPermission.Movies.Get, AspectPriority = 1)]
    public async Task<IDataResult<PageResultDto<ResponseMovieGetList>>> GetListAsync(RequestMovieGetList input)
    {
        var query = await (await _movieRepository.QueryableAsync()).Where(input);

        var result = new PageResultDto<ResponseMovieGetList>(
            query.Items.ToMapper<List<ResponseMovieGetList>>(),
            input.CurrentPage,
            input.CurrentPage);

        return new SuccessDataResult<PageResultDto<ResponseMovieGetList>>(result);
    }

    [AuthorizationAspect(TheMovieDbPermission.Movies.Get, AspectPriority = 1)]
    public async Task<IDataResult<ResponseMovieGetById?>> GetByIdAsync(RequestMovieGetById input)
    {
        //WithIncludeAsync: Include için Framework'üme eklediğim method.
        var query = await (await _movieRepository.WithIncludeAsync(x => x.Genres, x => x.BelongsToCollection)).FirstOrDefaultAsync(x => x.Id == input.Id);
        return new SuccessDataResult<ResponseMovieGetById?>(query?.ToMapper<ResponseMovieGetById>());
    }

    [FluentValidationAspect(typeof(MovieRecommendSelectedMovieValidator), AspectPriority = 2)]
    [AuthorizationAspect(TheMovieDbPermission.Movies.RecommendSelectedMovie, AspectPriority = 1)]
    public async Task<IResult> RecommendSelectedMovieAsync(RequestMovieRecommendSelectedMovie input)
    {
        var result = await _movieManager.RecommendSelectedMovieAsync(input);

        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }

        return new SuccessResult();

    }


    [FluentValidationAspect(typeof(MovieRateMovieValidator), AspectPriority = 2)]
    [AuthorizationAspect(TheMovieDbPermission.Movies.RecommendSelectedMovie, AspectPriority = 1)]
    public async Task<IResult> RateMovieAsync(RequestMovieRateMovie input)
    {
        var result = await _movieManager.RateMovieAsync(input);

        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }

        return new SuccessResult();

    }


}

