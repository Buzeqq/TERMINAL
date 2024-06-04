using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Exceptions;

internal class RecipeNotFoundException() : TerminalException("Recipe not found!");