using SimpleDataApi.Requests;
using SimpleDataApi.Responses;

namespace SimpleDataApi.Services
{
    public interface ICodeValuesService
    {
        public Task<IReadOnlyCollection<CodeValueResponse>> GetAsync(CodeValueFilter filter);

        public Task<int> AddRangeAsync(IEnumerable<CodeValueRequest> dto);
    }
}
