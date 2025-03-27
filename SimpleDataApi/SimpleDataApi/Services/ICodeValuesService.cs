using SimpleDataApi.Request;
using SimpleDataApi.Response;

namespace SimpleDataApi.Services
{
    public interface ICodeValuesService
    {
        public Task<IReadOnlyCollection<CodeValueResponseDto>> GetCodeValuesAsync(CodeValuePageFilter request);

        public Task<int> AddRangeAsync(IEnumerable<CodeValueRequestDto> dto);
    }
}
