namespace Terminal.Backend.Application.Common.QueryParameters;

public sealed class OrderingParameters
{
    public string OrderBy { get; set; } = "Id";

    public bool Desc { get; set; } = true;
}