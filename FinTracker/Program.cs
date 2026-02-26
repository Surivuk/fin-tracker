using FinTracker.Api.Transactions;
using FinTracker.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppServices();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGroup("/transactions").MapTransactions();
app.MapGroup("/categories").MapCategories();

app.Run();
