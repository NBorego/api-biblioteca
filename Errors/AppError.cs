using APIBiblioteca.Enums;

namespace APIBiblioteca.Errors
{
    public record AppError(string Detail, ErrorType ErrorType);
}
