namespace SimpleDataApi.Storage
{
    public class CodeValue
    {
        public int Id { get; set; }
        public int Code {  get; set; }
        public string Value { get; set; }

        public CodeValue(int code, string value)
        {
            Code = code;
            Value = value;
        }
    }
}
