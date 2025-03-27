using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleDataApi.Extensions;
using SimpleDataApi.Request;
using SimpleDataApi.Response;
using SimpleDataApi.Storage;

namespace SimpleDataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeValuesController : ControllerBase
    {
        private readonly AppDbContext context;

        public CodeValuesController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<PagedResponse<CodeValue>> GetAsync([FromQuery] PagedRequest request)
        {
            List<CodeValue> codeValues = await context.CodeValues
                .AsNoTracking()
                .SelectPage(request.PageSize, request.PageNumber)
                .ToListAsync();

            return new PagedResponse<CodeValue>(codeValues, codeValues.Count);
        }

        [HttpPost]
        public async Task<IActionResult> AddRangeAsync([FromBody] CodeValuesRequest request)
        {
            int addedRows = 0;
            IEnumerable<CodeValue> entities = MapCodeValuesToIndexed(request);

            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE dbo.CodeValues");
                await context.CodeValues.AddRangeAsync(entities);
                addedRows = await context.SaveChangesAsync();

                await transaction.CommitAsync();
            }

            return Ok($"Successfully saved {addedRows} rows");
        }

        private static IEnumerable<CodeValue> MapCodeValuesToIndexed(CodeValuesRequest request)
        {
            return request.CodeValues
                        .OrderBy(c => c.Code)
                        .Select((c, i) => new CodeValue(i, c.Code, c.Value));
        }
    }
}
