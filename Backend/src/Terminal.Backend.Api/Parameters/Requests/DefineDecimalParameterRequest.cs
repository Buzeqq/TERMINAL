namespace Terminal.Backend.Api.Parameters.Requests;

public record DefineDecimalParameterRequest(string Name, Guid? ParentId, string Unit, decimal Step, decimal? DefaultValue);
