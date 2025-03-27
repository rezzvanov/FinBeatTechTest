using Microsoft.AspNetCore.Mvc;
using SimpleDataApi.Request;
using SimpleDataApi.Response;
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
        public async Task<PagedResponse<CodeValueResponseDto>> GetAsync([FromQuery] CodeValuePageFilter request)
        {
            IReadOnlyCollection<CodeValueResponseDto> codeValuesDto = await codeValuesService.GetCodeValuesAsync(request);

            return new PagedResponse<CodeValueResponseDto>(codeValuesDto, codeValuesDto.Count);
        }

        [HttpPost]
        public async Task<IActionResult> AddRangeAsync([FromBody] CodeValuesRequest request)
        {
            int addedRows = await codeValuesService.AddRangeAsync(request.CodeValues);

            return Ok($"Successfully saved {addedRows} rows");
        }
    }
}
