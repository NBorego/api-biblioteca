using APIBiblioteca.DTO;
using APIBiblioteca.Services.Interfaces;

namespace APIBiblioteca.Routes
{
    public static class ReaderRoute
    {
        public static void ReaderRoutes(this WebApplication app)
        {
            var route = app.MapGroup("api/v1/reader");

            // GetAll
            route.MapGet("", async(int pageNumber, int pageQuantity, IReaderService service) =>
            {
                var readers = await service.GetAllAsync(pageNumber, pageQuantity);

                return Results.Ok(readers);
            });

            // GetById
            route.MapGet("{id:guid}", async (Guid id, IReaderService service) =>
            {
                var reader = await service.GetByIdAsync(id);

                return Results.Ok(reader);
            });

            // Save
            route.MapPost("", async (ReaderDTO dto, IReaderService service) =>
            {
                var readerResult = await service.SaveAsync(dto);

                if (readerResult.IsSuccess())
                {
                    var readerSuccess = readerResult.GetSuccessResult();

                    return Results.Created($"api/v1/users/{readerSuccess.Id}", readerSuccess);
                }

                var errorObj = readerResult.GetErrorResult();

                return Results.BadRequest(errorObj);
            });

            route.MapPut("{id:guid}", async (Guid id, ReaderDTO dto, IReaderService service) => 
            {
                var readerResult = await service.UpdateAsync(id, dto);

                if (readerResult.IsSuccess())
                {
                    var readerSuccess = readerResult.GetSuccessResult();

                    return Results.Ok(readerSuccess);
                }

                var errorObj = readerResult.GetErrorResult();

                return Results.BadRequest(errorObj);
            });

            route.MapDelete("{id:guid}", async (Guid id, IReaderService service) =>
            {
                await service.DeleteAsync(id);

                return Results.NoContent();
            });
        }
    }
}
