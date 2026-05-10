using EfCoreDDD.API;
using EfCoreDDD.Application.Interface;
using EfCoreDDD.Application.Interface.Queries;
using EfCoreDDD.Infrastructure;
using EfCoreDDD.Infrastructure.Database;
using EfCoreDDD.Infrastructure.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUnitOfWork,ApplicationDbContext>();
builder.Services.AddScoped<IWorkspaceQueries, WorkspaceQueriesContext>();
builder.Services.AddInfrastructure(builder.Configuration)
                .RegisterValidation();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapEndpoints(typeof(Program).Assembly);

app.Run();

