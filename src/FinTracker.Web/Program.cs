using FinTracker.DomainCore;
using FinTracker.IDomain;
using FinTracker.Persistence;
using FinTracker.Presentation;
using FinTracker.Presentation.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddPersistence(p => "5c44af57-ebdd-430f-b88e-6f890cda82d9");
builder.Services.AddDomainCoreModule();
builder.Services.AddPresentationModel();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.MapGet("/categories", async (GetUserCategories query) =>
{
    return Results.Json(await query.Execute());
})
.WithName("GetWeatherForecast");

app.Run();
