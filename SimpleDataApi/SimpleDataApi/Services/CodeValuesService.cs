using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleDataApi.Extensions;
using SimpleDataApi.Request;
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

        public async Task<IReadOnlyCollection<CodeValue>> GetCodeValuesAsync(PagedRequest request)
        {
            return await context.CodeValues
                .AsNoTracking()
                .SelectPage(request.PageSize, request.PageNumber)
                .ToListAsync();
        }

        public async Task<int> AddRangeAsync(IEnumerable<CodeValueDto> codeValueDtos)
        {
            int addedRows = 0;
            IEnumerable<CodeValue> codeValues = MapCodeValuesToIndexed(codeValueDtos);

            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE dbo.CodeValues");
                await context.CodeValues.AddRangeAsync(codeValues);
                addedRows = await context.SaveChangesAsync();

                await transaction.CommitAsync();
            }

            return addedRows;
        }

        private static IEnumerable<CodeValue> MapCodeValuesToIndexed(IEnumerable<CodeValueDto> codeValueDtos)
        {
            return codeValueDtos
                .OrderBy(c => c.Code)
                .Select((c, i) => new CodeValue(i, c.Code, c.Value));
        }
    }
}
