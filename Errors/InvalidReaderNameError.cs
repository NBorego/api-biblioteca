using APIBiblioteca.Enums;

namespace APIBiblioteca.Errors
{
    public record InvalidReaderNameError() :
        AppError("O nome do leitor deve conter somente letras e até 100 caracteres.", ErrorType.Validation);
}
