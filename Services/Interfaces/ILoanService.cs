using APIBiblioteca.DTO;
using APIBiblioteca.Errors;
using OneOf;

namespace APIBiblioteca.Services.Interfaces
{
    public interface ILoanService
    {
        Task<object> GetAllAsync(int pageNumber, int pageQuantity);
        Task<object?> GetAllByReaderIdAsync(Guid id, int pageNumber, int pageQuantity);
        Task<LoanDTO?> GetByIdAsync(Guid id);
        Task<OneOf<LoanDTO, AppError>> SaveAsync(LoanDTO dto);
        Task<DateTime?> UpdateReturnDateAsync(Guid id);
        Task<bool?> MarkAsReturnedAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}
