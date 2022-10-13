using Microsoft.AspNetCore.Mvc;
using TheMovieDb.Application.Contract.Movies;
using TheMovieDb.Application.Contract.Movies.Dtos;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using TheMovieDb.Application.Contract;
using Yella.Utilities.Results;
using TheMovieDb.Domain.Shared.Movies;
using IResult = Yella.Utilities.Results.IResult;

namespace TheMovieDb.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;
    private readonly IGenreService _genreService;
    public MovieController(IMovieService movieService, IGenreService genreService)
    {
        _movieService = movieService;
        _genreService = genreService;
    }



    /// <summary>
    /// Get a list of the current popular movies.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/movie/get-list?CurrentPage=1&amp;PageSize=15
    /// 
    ///     OR
    /// 
    ///     GET /api/movie/get-list
    ///     {
    ///        "CurrentPage": 1,    
    ///        "PageSize": 15
    ///     }
    ///
    /// </remarks>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<PageResultDto<ResponseMovieGetList>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> GetList([FromQuery] RequestMovieGetList input)
    {
        var result = await _movieService.GetListAsync(input);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Get the primary information about a movie.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/movie/get-by-id?Id=474D4D82-EC6E-4300-2176-08DA9956B81B
    /// 
    ///     OR
    /// 
    ///     GET /api/movie/get-by-id
    ///     {
    ///        "Id": "474D4D82-EC6E-4300-2176-08DA9956B81B",    
    ///     }
    ///
    /// </remarks>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<ResponseMovieGetById?>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> GetById([FromQuery] RequestMovieGetById input)
    {
        var result = await _movieService.GetByIdAsync(input);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<PageResultDto<IResult>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> RecommendSelectedMovie(RequestMovieRecommendSelectedMovie input)
    {
        var result = await _movieService.RecommendSelectedMovieAsync(input);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }



    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<PageResultDto<IResult>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> RateMovie(RequestMovieRateMovie input)
    {
        var result = await _movieService.RateMovieAsync(input);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }


}