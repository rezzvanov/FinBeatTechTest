namespace SimpleDataApi.Request
{
    public class CodeValuePageFilter : PagedRequest
    {
        public int? MinCode { get; set; }
        public int? MaxCode { get; set; }
        public string? ValueStartWith { get; set; }
    }
}
