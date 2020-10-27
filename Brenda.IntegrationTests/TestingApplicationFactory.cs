using Brenda.IntegrationTests.Infrastructure;
using Brenda.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;

namespace Brenda.IntegrationTests
{
    public class TestingApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        internal StubMessageHandler HttpMessageHandler { get; } = new StubMessageHandler();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType
                        == typeof(DbContextOptions<BrendaContext>));

                services.Remove(descriptor);
                services.AddDbContext<BrendaContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                services.AddHttpClient("with-proxy").ConfigurePrimaryHttpMessageHandler(() => HttpMessageHandler);

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<BrendaContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<TestingApplicationFactory<TStartup>>>();

                    db.Database.EnsureCreated();

                    try
                    {
                        Utilities.InitializeDbForTests(db);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                            "database with test messages. Error: {Message}", ex.Message);
                    }
                }

            });
        }

        internal class StubMessageHandler : HttpMessageHandler
        {
            public Queue<string> Responses { get; private set; } = new Queue<string>();

            protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                if (Responses.TryDequeue(out var response))
                {
                    HttpResponseMessage httpResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                    {
                        Content = new StringContent(response)
                    };
                    Responses.Enqueue(response);
                    return await Task.FromResult(httpResponse);
                }

                throw new IndexOutOfRangeException();
            }
        }
    }

    public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new[] { new Claim(ClaimTypes.Name, "Test user") };
            ClaimsIdentity identity = new ClaimsIdentity(claims, "Test");
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            AuthenticationTicket ticket = new AuthenticationTicket(principal, "Test");

            AuthenticateResult result = AuthenticateResult.Success(ticket);

            return Task.FromResult(result);
        }
    }



}
