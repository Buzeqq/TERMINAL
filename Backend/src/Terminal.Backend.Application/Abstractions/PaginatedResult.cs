namespace Terminal.Backend.Application.Abstractions;

public abstract record PaginatedResult<T>(IEnumerable<T> Data, int TotalCount, int PageIndex, int PageSize);
