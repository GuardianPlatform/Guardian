using AutoMapper;
using Guardian.Domain.Settings;
using Guardian.Infrastructure.Database;
using Guardian.Infrastructure.EventHub;
using Guardian.Infrastructure.Mapping;
using Guardian.Service.Contract;
using Guardian.Service.Identity;
using Guardian.Service.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace Guardian.Infrastructure.Extension
{
    public static class ConfigureServiceContainer
    {
        public static IServiceCollection AddDbContext(this IServiceCollection serviceCollection,
             IConfiguration configuration, IConfigurationRoot configRoot)
        {
            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                   options.UseSqlServer(configuration.GetConnectionString("OnionArchConn") ?? configRoot["ConnectionStrings:OnionArchConn"],
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            serviceCollection.AddDbContext<IdentityContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("OnionArchConn") ?? configRoot["ConnectionStrings:OnionArchConn"],
            b => b.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));

            return serviceCollection;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection serviceCollection)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CustomerProfile());
            });
            var mapper = mappingConfig.CreateMapper();
            return serviceCollection.AddSingleton(mapper);
        }

        public static IServiceCollection AddEventHub(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddSingleton(typeof(IEventHubBuilder<>), typeof(EventHubBuilder<>));
        }

        public static IServiceCollection AddScopedServices(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
        }

        public static IServiceCollection AddTransientServices(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddTransient<IDateTimeService, DateTimeService>()
                .AddTransient<IAccountService, AccountService>();
        }

        public static IServiceCollection AddSwaggerOpenAPI(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                    "OpenAPISpecification",
                    new OpenApiInfo()
                    {
                        Title = "Onion Architecture WebAPI",
                        Version = "1",
                        Description = "Through this API you can access customer details",
                        License = new OpenApiLicense()
                        {
                            Name = "MIT License",
                            Url = new Uri("https://opensource.org/licenses/MIT")
                        }
                    });

                setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = $"Input your Bearer token in this format - Bearer token to access this API",
                });
                
                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                        }, new List<string>()
                    },
                });
            });

            return serviceCollection;
        }

        public static IServiceCollection AddMailSetting(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            return serviceCollection.Configure<MailSettings>(
                configuration.GetSection("MailSettings"));
        }

        public static IServiceCollection AddEventHubSettings(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            return serviceCollection.Configure<EventHubSettings>(
                configuration.GetSection(EventHubSettings.EventHubSettingsName));
        }

        public static IServiceCollection AddController(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddControllers()
                .AddNewtonsoftJson();
            
            return serviceCollection;
        }

        public static IServiceCollection AddVersion(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });

            return serviceCollection;
        }

        public static IServiceCollection AddHealthCheck(this IServiceCollection serviceCollection, AppSettings appSettings, IConfiguration configuration)
        {
            serviceCollection
                .AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>(name: "Application DB Context", failureStatus: HealthStatus.Degraded)
                .AddSqlServer(configuration.GetConnectionString("OnionArchConn"));

            serviceCollection.AddHealthChecksUI(setupSettings: setup =>
            {
                setup.AddHealthCheckEndpoint("Basic Health Check", $"/healthz");
            }).AddInMemoryStorage();

            return serviceCollection;
        }
    }
}
