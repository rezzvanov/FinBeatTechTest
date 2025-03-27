namespace SimpleDataApi.Requests
{
    public class CodeValueFilter : PagedRequest
    {
        public int? MinCode { get; set; }
        public int? MaxCode { get; set; }
        public string? ValuePrefix { get; set; }
    }
}
