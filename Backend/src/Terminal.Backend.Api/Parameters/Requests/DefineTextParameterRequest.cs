namespace Terminal.Backend.Api.Parameters.Requests;

public record DefineTextParameterRequest(string Name, Guid? ParentId, string[] AllowedValues, uint? DefaultValue);
