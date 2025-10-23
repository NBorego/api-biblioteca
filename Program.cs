var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.AddDatabase();
builder.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.AddRoutes();

app.UseHttpsRedirection();

app.Run();
