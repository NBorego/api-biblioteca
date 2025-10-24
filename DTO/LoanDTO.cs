namespace APIBiblioteca.DTO
{
    public record LoanDTO(Guid Id, string BookName, DateTime ReturnDate, bool Returned, Guid ReaderId);
}
