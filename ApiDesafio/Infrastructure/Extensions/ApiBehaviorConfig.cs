using Microsoft.AspNetCore.Mvc;

namespace ApiDesafio.Infrastructure.Extensions
{
    public static class ApiBehaviorConfig
    {
        public static void ConfigureInvalidModelState(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value?.Errors.Count > 0)
                        .SelectMany(e => e.Value!.Errors.Select(err => new
                        {
                            campo = e.Key,
                            mensagem = err.ErrorMessage
                        }))
                        .ToArray();

                    var response = new
                    {
                        statusCode = 400,
                        message = "Dados inválidos",
                        errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });
        }
    }
}
