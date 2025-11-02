using APIBiblioteca.DTO;
using APIBiblioteca.Services.Interfaces;
using System.IO.Pipelines;

namespace APIBiblioteca.Routes
{
    public static class LoanRoute
    {
        public static void LoanRoutes(this WebApplication app)
        {
            var route = app.MapGroup("/api/v1/loan");

            // GetAll
            route.MapGet("", async (int pageNumber, int pageQuantity, string bookName, ILoanService service) =>
            {
                var loans = await service.GetAllAsync(pageNumber, pageQuantity, bookName);

                return loans;
            });

            // GetAllByReaderId
            route.MapGet("{readerId:guid}/loans", async (Guid readerId, int pageNumber, int pageQuantity, string bookName, ILoanService service) =>
            {
                var loans = await service.GetAllByReaderIdAsync(readerId, pageNumber, pageQuantity, bookName);

                if (loans == null)
                    return Results.NotFound();

                return Results.Ok(loans);
            });

            // GetById
            route.MapGet("{id:guid}", async (Guid id, ILoanService service) =>
            {
                var loan = await service.GetByIdAsync(id);
                
                if (loan == null)
                    return Results.NotFound();

                return Results.Ok(loan); ;
            });

            // Save
            route.MapPost("", async (LoanDTO dto, ILoanService service) =>
            {
                var loanResult = await service.SaveAsync(dto);

                if (loanResult.IsSuccess())
                {
                    var loanSuccess = loanResult.GetSuccessResult();

                    return Results.Created($"api/v1/loan/{loanSuccess.Id}", loanSuccess);
                }

                var errorObj = loanResult.GetErrorResult();

                return Results.BadRequest(errorObj);
            });

            // UpdateReturnDate
            route.MapPut("{id:guid}", async (Guid id, ILoanService service) =>
            {
                var loan = await service.UpdateReturnDateAsync(id);

                if (loan == null)
                    return Results.NotFound();

                return Results.Ok(loan);
            });

            // MarkAsReturned
            route.MapPut("{id:guid}/returned", async (Guid id, ILoanService service) =>
            {
                var loan = await service.MarkAsReturnedAsync(id);

                if (loan == null)
                    return Results.NotFound();

                return Results.Ok(loan);
            });

            // Delete
            route.MapDelete("{id:guid}", async (Guid id, ILoanService service) =>
            {
                await service.DeleteAsync(id);

                return Results.NoContent();
            });
        }
    }
}
