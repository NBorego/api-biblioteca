using APIBiblioteca.Enums;

namespace APIBiblioteca.Errors
{
    public record InvalidReaderPhoneError() : AppError("Telefone deve ter somente 11 digitos!", ErrorType.Validation);
}
