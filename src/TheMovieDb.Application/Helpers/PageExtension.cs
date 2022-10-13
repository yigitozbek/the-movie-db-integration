using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using TheMovieDb.Application.Contract;
using Yella.Domain.Entities;

namespace TheMovieDb.Application.Helpers;

public static class PageExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="source"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public static async Task<PageResultDto<TEntity>> Where<TEntity>(
        this IQueryable<TEntity> source, PagedResultBase input
        )
        where TEntity : Entity
    {
        var query = source
            .Skip(input.PageSize * (input.CurrentPage - 1))
            .Take(input.PageSize);

        var result = await query.ToListAsync();

        return new PageResultDto<TEntity>(result, input.CurrentPage, source.Count());
    }

}

