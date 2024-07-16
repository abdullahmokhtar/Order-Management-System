using Microsoft.OpenApi.Models;

namespace RouteSummitTask.PL.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Order Management System",
                    Version = "v1",
                    Description = "Order Management System",
                    Contact = new OpenApiContact
                    {
                        Name = "Abdullah Mokhtar",
                        Email = "abdullahmokhtr55.com",
                    }
                });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the bearer schema. Example: 'Authorization. Bearer {token}'"
                    ,
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "bearer"
                    }
                };

                options.AddSecurityDefinition("bearer", securitySchema);

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securitySchema, new[]{"bearer"} }
                });
            });

            return services;
        }
    }
}
