using Microsoft.AspNetCore.Mvc;
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

        public async Task<IReadOnlyCollection<CodeValueResponseDto>> GetCodeValuesAsync(PagedRequest request)
        {
            return await context.CodeValues
                .AsNoTracking()
                .SelectPage(request.PageSize, request.PageNumber)
                .Select(c => new CodeValueResponseDto(c.Index, c.Code, c.Value))
                .ToListAsync();
        }

        public async Task<int> AddRangeAsync(IEnumerable<CodeValueRequestDto> codeValueDtos)
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

        private static IEnumerable<CodeValue> MapCodeValuesToIndexed(IEnumerable<CodeValueRequestDto> codeValueDtos)
        {
            return codeValueDtos
                .OrderBy(c => c.Code)
                .Select((c, i) => new CodeValue(i, c.Code, c.Value));
        }
    }
}
