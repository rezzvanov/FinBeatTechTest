using Microsoft.EntityFrameworkCore;
using SimpleDataApi.Extensions;
using SimpleDataApi.Request;
using SimpleDataApi.Response;
using SimpleDataApi.Storage;

namespace SimpleDataApi.Services
{
    public class CodeValuesService : ICodeValuesService
    {
        private readonly AppDbContext context;

        public CodeValuesService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IReadOnlyCollection<CodeValueResponse>> GetCodeValuesAsync(CodeValuePageFilter request)
        {
            return await context.CodeValues
                .AsNoTracking()
                .AddFilterIfSet(request.MinCode.HasValue, c => c.Code <= request.MinCode)
                .AddFilterIfSet(request.MaxCode.HasValue, c => c.Code >= request.MaxCode)
                .AddFilterIfSet(!string.IsNullOrEmpty(request.ValueStartWith), c => c.Value.StartsWith(request.ValueStartWith))
                .SelectPage(request.PageSize, request.PageNumber)
                .Select(c => new CodeValueResponse(c.Id, c.Code, c.Value))
                .ToListAsync();
        }

        public async Task<int> AddRangeAsync(IEnumerable<CodeValueRequestDto> codeValueDtos)
        {
            int addedRows = 0;
            IEnumerable<CodeValue> codeValues = MapRequestDtoToEntity(codeValueDtos);

            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE dbo.CodeValues");
                await context.CodeValues.AddRangeAsync(codeValues);
                addedRows = await context.SaveChangesAsync();

                await transaction.CommitAsync();
            }

            return addedRows;
        }

        private static IEnumerable<CodeValue> MapRequestDtoToEntity(IEnumerable<CodeValueRequestDto> codeValueDtos)
        {
            return codeValueDtos
                .OrderBy(c => c.Code)
                .Select((c) => new CodeValue(c.Code, c.Value));
        }
    }
}
