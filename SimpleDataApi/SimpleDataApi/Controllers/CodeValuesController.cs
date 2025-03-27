using Microsoft.AspNetCore.Mvc;
using SimpleDataApi.Requests;
using SimpleDataApi.Responses;
using SimpleDataApi.Services;

namespace SimpleDataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeValuesController : ControllerBase
    {
        private readonly ICodeValuesService codeValuesService;

        public CodeValuesController(ICodeValuesService codeValuesService)
        {
            this.codeValuesService = codeValuesService;
        }

        [HttpGet]
        public async Task<PagedResponse<CodeValueResponse>> GetAsync([FromQuery] CodeValueFilter request)
        {
            IReadOnlyCollection<CodeValueResponse> codeValues = await codeValuesService.GetAsync(request);

            return new PagedResponse<CodeValueResponse>(codeValues, codeValues.Count);
        }

        [HttpPost]
        public async Task<IActionResult> AddRangeAsync([FromBody] CodeValuesRequest request)
        {
            int addedRows = await codeValuesService.AddRangeAsync(request.CodeValues);

            return Ok($"Successfully saved {addedRows} rows");
        }
    }
}
