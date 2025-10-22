using APIBiblioteca.Enums;

namespace APIBiblioteca.Errors
{
    public record InvalidReaderName() : AppError("O nome do leitor deve conter apenas letras!", ErrorType.Validation);
}
