namespace TaskManagement.Api.Dependencies;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddCorsForBuilder();
        services.AddSwaggerGenAuthorization();

        services.AddSingleton<ProblemDetailsFactory, TaskManagementProblemDetailsFactory>();
        return services;
    }

    public static IServiceCollection AddCorsForBuilder(this IServiceCollection services)
    {
        services.AddCors(option =>
        {
            option.AddDefaultPolicy(b =>
            {
                b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });

        return services;
    }

    public static IServiceCollection AddSwaggerGenAuthorization(this IServiceCollection services)
    {
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc(SwaggerSettings.Version, new OpenApiInfo { Title = SwaggerSettings.Title, Version = SwaggerSettings.Version });
            opt.AddSecurityDefinition(SwaggerSettings.Type, new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = SwaggerSettings.Description,
                Name = SwaggerSettings.Name,
                Type = SecuritySchemeType.Http,
                BearerFormat = SwaggerSettings.BearerFormat,
                Scheme = SwaggerSettings.Scheme
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id= SwaggerSettings.Id
                        }
                    },
                    new string[]{}
                }
            });
        });

        return services;
    }
}
