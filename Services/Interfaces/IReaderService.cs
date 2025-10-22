using APIBiblioteca.DTO;
using APIBiblioteca.Errors;
using OneOf;

namespace APIBiblioteca.Services.Interfaces
{
    public interface IReaderService
    {
        Task<object> GetAllAsync(int pageNumber, int pageQuantity);
        Task<ReaderDTO?> GetByIdAsync(Guid id);
        Task<OneOf<ReaderDTO, AppError>> SaveAsync(ReaderDTO dto);
        Task<OneOf<ReaderDTO, AppError>> UpdateAsync(Guid id, ReaderDTO dto);
        Task DeleteAsync(Guid id);
    }
}
