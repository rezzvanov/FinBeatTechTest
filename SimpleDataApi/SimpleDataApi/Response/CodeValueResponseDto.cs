namespace SimpleDataApi.Response
{
    public class CodeValueResponseDto
    {
        public int Index { get; set; }
        public int Code { get; set; }
        public string? Value { get; set; }

        public CodeValueResponseDto(int index, int code, string? value)
        {
            Index = index;
            Code = code;
            Value = value;
        }
    }
}
