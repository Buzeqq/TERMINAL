namespace Terminal.Backend.Api.Parameters.Requests;

public record DefineIntegerParameterRequest(string Name, Guid? ParentId, string Unit, int Step, int? DefaultValue);
