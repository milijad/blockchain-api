using Blockchain.Api.Common.Configuration;
using Blockchain.Api.Extensions;
using Blockchain.Api.Health;
using Blockchain.Api.Middleware;
using Blockchain.Application;
using Blockchain.Application.Common;
using Blockchain.Infrastructure;
using Blockchain.Infrastructure.Configuration;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<BlockCypherOptions>(
    builder.Configuration.GetSection(BlockCypherConstants.ConfigurationSection));
var corsOptions = builder.Configuration
    .GetSection(CorsOptions.SectionName)
    .Get<CorsOptions>();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<AssemblyReference>());
builder.Services.AddApplicationHealthChecks(builder.Configuration);

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

builder.Services.AddCors(options =>
{
    options.AddPolicy("InternalOnly", p =>
        p.WithOrigins(corsOptions!.AllowedOrigins)
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseCors("InternalOnly");
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Blockchain Snapshot API v1");
});
app.MapApplicationHealthChecks();
app.MapAllEndpoints();

app.Run();
