using Terminal.Backend.Application.Abstractions;

namespace Terminal.Backend.Application.Commands;

public sealed record CreateProjectCommand([property: System.Text.Json.Serialization.JsonIgnore] Guid Id, string ProjectName) : ICommand;