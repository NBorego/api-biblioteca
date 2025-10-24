using APIBiblioteca.Routes;

namespace APIBiblioteca.Extensions
{
    public static class AppExtensisons
    {
        public static void AddRoutes(this WebApplication app)
        {
            app.ReaderRoutes();
            app.LoanRoutes();
        }
    }
}
