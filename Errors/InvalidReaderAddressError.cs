using APIBiblioteca.Enums;

namespace APIBiblioteca.Errors
{
    public record InvalidReaderAddressError()
        : AppError("O endereço deve conter no máximo 150 caracteres.", ErrorType.Validation);
}
