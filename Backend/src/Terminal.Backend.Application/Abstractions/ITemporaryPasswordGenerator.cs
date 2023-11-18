using Terminal.Backend.Application.DTO;

namespace Terminal.Backend.Application.Abstractions;

internal interface ITemporaryPasswordGenerator
{
    TemporaryPassword Generate();
}