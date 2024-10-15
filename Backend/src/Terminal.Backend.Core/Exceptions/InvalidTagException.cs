using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Exceptions;

public sealed class InvalidTagException(TagName tag) : TerminalException($"Unable to create tag with name {tag}!");
