using ApiDesafio.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using ApiDesafio.Application.Extensions;
using ApiDesafio.API.Configuration;
using ApiDesafio.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureInvalidModelState();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular4200",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

/* DATABASE */
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=db/ApiDesafio.db"));

/* SERVICES */
builder.Services.AddApplicationServices();

/* JWT */
builder.Services.AddJwtAuthentication(builder.Configuration);

/* SWAGGER */
builder.Services.AddSwaggerDocumentation();


var app = builder.Build();

app.UseMiddleware<ApiExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseCors("AllowAngular4200");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ApiExceptionMiddleware>();

app.Run();
