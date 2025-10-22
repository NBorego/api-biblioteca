using APIBiblioteca.Enums;

namespace APIBiblioteca.Errors
{
    public record InvalidReaderPhone() : AppError("Telefone deve ter somente 11 digitos!", ErrorType.Validation);
}
