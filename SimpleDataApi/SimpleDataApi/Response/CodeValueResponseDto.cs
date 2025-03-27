namespace SimpleDataApi.Response
{
    public class CodeValueResponseDto
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Value { get; set; }

        public CodeValueResponseDto(int id, int code, string value)
        {
            Id = id;
            Code = code;
            Value = value;
        }
    }
}
