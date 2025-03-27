﻿using Microsoft.AspNetCore.Mvc;
using SimpleDataApi.Request;
using SimpleDataApi.Storage;

namespace SimpleDataApi.Services
{
    public interface ICodeValuesService
    {
        public Task<IReadOnlyCollection<CodeValueResponseDto>> GetCodeValuesAsync(PagedRequest request);

        public Task<int> AddRangeAsync(IEnumerable<CodeValueRequestDto> dto);
    }
}
