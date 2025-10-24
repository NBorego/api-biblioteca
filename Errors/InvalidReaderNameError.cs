using APIBiblioteca.Enums;

namespace APIBiblioteca.Errors
{
    public record InvalidReaderNameError() :
        AppError(
            "O nome do leitor deve conter apenas letras, ter no máximo 100 caracteres e não pode ficar em branco.", 
            ErrorType.Validation
        );
}
