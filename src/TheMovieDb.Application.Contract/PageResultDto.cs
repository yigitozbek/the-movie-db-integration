namespace TheMovieDb.Application.Contract;

public class PageResultDto<T> : PagedResultBase
{
    public int TotalCount { get; set; }

    public PageResultDto(List<T> items, short currentPage, int totalCount)
    {
        Items = items;
        CurrentPage = currentPage;
        TotalCount = totalCount;
    }
    public int LastRowOnPage => Math.Min(CurrentPage * PageSize, TotalCount);
    public int FirstRowOnPage => (CurrentPage - 1) * PageSize + 1;

    public List<T> Items { get; set; }

}