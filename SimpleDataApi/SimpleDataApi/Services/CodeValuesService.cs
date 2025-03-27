using Microsoft.EntityFrameworkCore;
using SimpleDataApi.Extensions;
using SimpleDataApi.Requests;
using SimpleDataApi.Responses;
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

        public async Task<IReadOnlyCollection<CodeValueResponse>> GetAsync(CodeValueFilter filter)
        {
            return await context.CodeValues
                .AsNoTracking()
                .AddFilterIfSet(filter.MinCode.HasValue, c => c.Code <= filter.MinCode)
                .AddFilterIfSet(filter.MaxCode.HasValue, c => c.Code >= filter.MaxCode)
                .AddFilterIfSet(!string.IsNullOrEmpty(filter.ValuePrefix), c => c.Value.StartsWith(filter.ValuePrefix!))
                .SelectPage(filter.PageSize, filter.PageNumber)
                .Select(c => new CodeValueResponse(c.Id, c.Code, c.Value))
                .ToListAsync();
        }

        public async Task<int> AddRangeAsync(IEnumerable<CodeValueRequest> records)
        {
            int addedRows = 0;
            IEnumerable<CodeValue> codeValues = MapRequestToEntities(records);

            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE dbo.CodeValues");
                await context.CodeValues.AddRangeAsync(codeValues);
                addedRows = await context.SaveChangesAsync();

                await transaction.CommitAsync();
            }

            return addedRows;
        }

        private static IEnumerable<CodeValue> MapRequestToEntities(IEnumerable<CodeValueRequest> records)
        {
            return records
                .OrderBy(c => c.Code)
                .Select(c => new CodeValue(c.Code, c.Value));
        }
    }
}
