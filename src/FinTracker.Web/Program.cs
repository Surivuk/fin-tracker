using FinTracker.DomainCore;
using FinTracker.Persistence;
using FinTracker.Presentation;
using FinTracker.Transaction;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUserIdProvider, UserIdProvider>();
builder.Services.AddPersistence(p => p.GetRequiredService<IUserIdProvider>().GetUserId());

builder.Services.AddDomainCoreModule();
builder.Services.AddTransactionModel();
builder.Services.AddPresentationModel();

builder.Services.AddApiAuth();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.MapOpenApi();

// app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


var api = app.MapGroup("/api").RequireAuthorization();

api.MapGroup("/categories").MapCategories();
api.MapGet("/ping", () => Results.Ok(new { message = "PONG" }));

app.Run();
