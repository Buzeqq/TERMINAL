using Terminal.Backend.Application.Common.QueryParameters;

namespace Terminal.Backend.Application.Abstractions;

public abstract record PaginatedResult<T>(IEnumerable<T> Data, int TotalCount, PagingParameters PagingParameters);
