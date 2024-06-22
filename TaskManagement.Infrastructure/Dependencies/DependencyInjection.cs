namespace TaskManagement.Infrastructure.Dependencies;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddSqlServer(configuration);

        services.AddSettings(configuration);

        services.AddScopedServices();

        services.AddSingeltonServices();

        services.AddJwtTokenAuth(configuration);

        services.InitializeDatabase();

        services.InitializeRedis(configuration);

        return services;
    }

    private static IServiceCollection AddSettings(this IServiceCollection services, ConfigurationManager configuration) 
    {
        var jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
        services.AddSingleton(jwtSettings!);

        var redisSettings = configuration.GetSection(nameof(RedisSettings)).Get<RedisSettings>();
        services.AddSingleton(redisSettings!);

        var authSettings = configuration.GetSection(nameof(AuthorizationSettings)).Get<AuthorizationSettings>();
        services.AddSingleton(authSettings!);

        return services;
    }

    private static IServiceCollection AddSingeltonServices(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordService, PasswordService>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }

    private static IServiceCollection AddScopedServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAssignmentRepository, AssignmentRepository>();

        services.AddScoped<IRedisManager, RedisManager>();
        services.AddScoped<IAuthorizationService, AuthorizationService>();


        return services;
    }

    private static IServiceCollection AddJwtTokenAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        var jwtSetting = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>()!;

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSetting.Issuer,
                    ValidAudience = jwtSetting.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSetting.Secret)),
                };
            });

        return services;
    }

    private static IServiceCollection AddSqlServer(this IServiceCollection services, ConfigurationManager configuration)
    {
        var connectionStringOptions = configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();

        services.AddDbContext<TaskManagementContext>(options =>
            options.UseSqlServer(connectionStringOptions!.SQLServer));

        return services;
    }

    private static IServiceCollection InitializeDatabase(this IServiceCollection services)
    {
        var sp = services.BuildServiceProvider();
        var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        using var context = sp.GetService(typeof(TaskManagementContext)) as TaskManagementContext;
        context!.Database.Migrate();

        return services;
    }

    private static IServiceCollection InitializeRedis(this IServiceCollection services, ConfigurationManager configuration)
    {
        var redisOptions = configuration.GetSection(nameof(RedisSettings)).Get<RedisSettings>()!;

        SessionBaseManager.ExpirationTime = TimeSpan.FromMinutes(redisOptions.SessionTimeout);
        var redisConnection = redisOptions.ConnectionString;
        var password = redisOptions.Password;

        if (redisConnection.Contains(';'))
        {
            var redishosts = redisConnection.Split(";");
            RedisManager.Initialize(password, true, redishosts);

            return services;
        }
        RedisManager.Initialize(password, true, redisConnection);

        return services;
    }
}