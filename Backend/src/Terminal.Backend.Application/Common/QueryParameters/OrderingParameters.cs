namespace Terminal.Backend.Application.Common.QueryParameters;

public sealed record OrderingParameters(string OrderBy, OrderDirection? Direction);

public enum OrderDirection
{
    Ascending,
    Descending
}
