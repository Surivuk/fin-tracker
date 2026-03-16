using FinTracker.DomainCore;
using FinTracker.Persistence;
using FinTracker.Presentation;
using FinTracker.Transaction;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddAuth();

builder.Services.AddDomainCoreModule();
builder.Services.AddTransactionModel();
builder.Services.AddPresentationModel();
builder.Services.AddPersistence(p => p.GetRequiredService<IUserIdProvider>().GetUserId());

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseAuthentication();
app.UseAuthorization();

var api = app.MapGroup("/api").RequireAuthorization();

api.MapGroup("/categories")
    .MapCategories()
    .MapGroup("/{id}")
        .RequireAuthorization("CategoryOwner")
        .MapCategory()
        .MapGroup("/transactions")
            .MapTransactions()
            .MapGroup("/{transactionId}").MapTransaction();

api.MapGet("/ping", () => Results.Ok(new { message = "PONG" }));

app.Run();
