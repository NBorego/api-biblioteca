var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.AddDatabase();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
