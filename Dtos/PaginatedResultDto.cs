public class PaginatedResultDto<T>
{
    public IEnumerable<T> result { get; set; }
    public int totalRecordsCount { get; set; }

    public PaginatedResultDto(IEnumerable<T> result, int totalRecordsCount)
    {
        this.result = result;
        this.totalRecordsCount = totalRecordsCount;
    }
}