using APIBiblioteca.Enums;

namespace APIBiblioteca.Errors
{
    public record InvalidBookNameError() 
        : AppError("O nome do livro deve ter até 100 caracteres e não pode ficar em branco.", ErrorType.Validation);
}
