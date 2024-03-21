namespace Terminal.Backend.Application.Common.QueryParameters;

public sealed class PagingParameters
{
    private const int MaxPageSize = 50;
    private int _pageSize = 10;

    public int PageNumber { get; set; } = 1;

    public int PageSize
    {
        get => this._pageSize;
        set => this._pageSize = Math.Min(MaxPageSize, value);
    }
}