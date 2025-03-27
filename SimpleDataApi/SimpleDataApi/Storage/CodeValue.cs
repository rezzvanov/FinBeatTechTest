namespace SimpleDataApi.Storage
{
    public class CodeValue
    {
        public int Index { get; set; }
        public int Code {  get; set; }
        public string Value { get; set; }

        public CodeValue(int index, int code, string value)
        {
            Index = index;
            Code = code;
            Value = value;
        }
    }
}
