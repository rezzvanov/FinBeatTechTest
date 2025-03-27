namespace SimpleDataApi.Responses
{
    public class CodeValueResponse
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Value { get; set; }

        public CodeValueResponse(int id, int code, string value)
        {
            Id = id;
            Code = code;
            Value = value;
        }
    }
}
