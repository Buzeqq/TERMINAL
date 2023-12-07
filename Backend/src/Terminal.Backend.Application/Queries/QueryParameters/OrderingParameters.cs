namespace Terminal.Backend.Application.Queries.QueryParameters;

public sealed class OrderingParameters
{
    public string OrderBy { get; set; } = "Id";

    public bool Desc { get; set; } = true;
}