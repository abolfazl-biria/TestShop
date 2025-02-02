namespace Common.Dtos;

public abstract class PaginationDto(int page, int pageSize, bool pagination = true)
{
    private const int MinPageNumber = 0;
    private const int MaxPageSize = 10;

    public bool Pagination { get; } = pagination;
    public int Page { get; } = page >= 0 ? page : MinPageNumber;
    public int PageSize { get; } = pageSize > 0 ? pageSize : MaxPageSize;
}