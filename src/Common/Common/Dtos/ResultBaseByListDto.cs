namespace Common.Dtos;

public class ResultBaseByListDto<T>(List<T>? result, int count, int pageCount)
{
    public List<T>? Result { get; set; } = result;
    public int Count { get; set; } = count;
    public int PageCount { get; set; } = pageCount;
}