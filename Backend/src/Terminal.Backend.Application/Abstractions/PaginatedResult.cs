using Terminal.Backend.Application.Common.QueryParameters;

namespace Terminal.Backend.Application.Abstractions;

public abstract record PaginatedResult(int TotalCount, PagingParameters PagingParameters);
