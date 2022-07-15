using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Castle.Core.Internal;
using FluentAssertions.Common;
using Guardian.Domain.Enum;
using Guardian.Infrastructure.Communication;
using Guardian.Infrastructure.Database;
using Guardian.Test.Integration.WebFactory.UserClaims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using LogLevel = Guardian.Logging.Api.LogLevel;

namespace Guardian.Test.Integration.WebFactory
{
    public class GuardianWebApplicationFactory : WebApplicationFactory<Startup>
    {
        private IUserClaims _authorizationUserClaims = new AuthorizedUserClaims();

        public void SetAuthorizationRoles(IEnumerable<Roles> customRoles)
        {
            var roleClaims = customRoles.Select(x => new Claim(ClaimTypes.Role, x.ToString()));
            SetAuthorization(new CustomUserClaims(roleClaims));
        }

        public void SetAuthorization(IUserClaims authorizationUserClaims)
        {
            _authorizationUserClaims = authorizationUserClaims;
        }

        protected override void ConfigureClient(HttpClient client)
        {
            base.ConfigureClient(client);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptorApplication = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<ApplicationDbContext>));

                var descriptorIdentity = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<IdentityContext>));


                services.Remove(descriptorApplication);
                services.Remove(descriptorIdentity);

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                services.AddDbContext<IdentityContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var applicationContext = scopedServices.GetRequiredService<ApplicationDbContext>();
                    var identityContext = scopedServices.GetRequiredService<IdentityContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<GuardianWebApplicationFactory>>();

                    applicationContext.Database.EnsureCreated();
                    identityContext.Database.EnsureCreated();

                    try
                    {
                        DatabaseSeeding.ApplicationContext(applicationContext);
                        DatabaseSeeding.IdentityContext(identityContext);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            "database with test messages. Error: {Message}", ex.Message);
                    }
                }
            }).ConfigureTestServices(x =>
            {
                var loggingApiClient = new Mock<ILoggingApiClient>();
                loggingApiClient.Setup(x => x.Log(
                        It.IsAny<string>(),
                        It.IsAny<DateTime>(),
                        It.IsAny<LogLevel>(),
                        It.IsAny<CancellationToken>()))
                    .ReturnsAsync(() => HttpStatusCode.OK);

                x.AddSingleton(loggingApiClient.Object);

                x.AddSingleton(typeof(IUserClaims), _authorizationUserClaims);

                x.AddAuthentication(x =>
                    {
                        x.DefaultAuthenticateScheme = "Test";
                        x.DefaultChallengeScheme = "Test";
                    })
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                        "Test", options => { });

            });
        }
    }
}
