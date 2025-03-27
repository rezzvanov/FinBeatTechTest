namespace SimpleDataApi.Responses
{
    public class PagedResponse<T>
    {
        public int Count { get; set; }

        public IReadOnlyCollection<T> Data { get; set; }

        public PagedResponse(IReadOnlyCollection<T> data, int count)
        {
            Data = data;
            Count = count;
        }
    }
}
