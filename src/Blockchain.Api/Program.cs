using Blockchain.Api.Endpoints;
using Blockchain.Api.Middleware;
using Blockchain.Infrastructure;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Blockchain Snapshot API",
        Version = "v1",
        Description = "Collects and stores blockchain snapshots from BlockCypher"
    });
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Blockchain Snapshot API v1");
});

app.UseHttpsRedirection();
app.MapBlockchainEndpoints();

app.Run();
