namespace SimpleDataApi.Requests
{
    public class CodeValueRequest
    {
        public int Code { get; set; }
        public required string Value { get; set; }
    }
}
