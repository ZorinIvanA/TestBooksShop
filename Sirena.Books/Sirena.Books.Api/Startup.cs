using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sirena.Books.Api.CustomAuth;
using Sirena.Books.Domain.Interfaces;
using Sirena.Books.Domain.Services;
using Sirena.Books.Infrastructure.Configuration;
using Sirena.Books.Infrastructure.Repositories;
using ZNetCS.AspNetCore.Authentication.Basic;
using ZNetCS.AspNetCore.Authentication.Basic.Events;

namespace Sirena.Books.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<ISummaryService, SummaryService>();
            services.AddScoped<IBooksRepository, BooksRepository>();
            //services.AddSingleton(Configuration);
            services.Configure<DbInfo>(options => Configuration
                .GetSection("ConnectionStrings").Bind(options));

            EnCrutchBasicAuth(services);
        }

        private void EnCrutchBasicAuth(IServiceCollection services)
        {
            services
                .AddAuthorization(options =>
                {
                    options.AddPolicy(AuthConstants.CUSTOMER_ROLE, policy =>
                        policy.RequireClaim(ClaimTypes.Role, AuthConstants.CUSTOMER_ROLE));
                    options.AddPolicy(AuthConstants.ADMIN_ROLE, policy =>
                        policy.RequireClaim(ClaimTypes.Role, AuthConstants.ADMIN_ROLE));
                })
                .AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme)
                .AddBasicAuthentication(
                    options =>
                    {
                        options.Realm = "Sirena.Books";
                        options.Events = new BasicAuthenticationEvents
                        {
                            OnValidatePrincipal = context =>
                            {
                                if (context.UserName.Equals("Ivanov", StringComparison.OrdinalIgnoreCase)
                                    && (context.Password.Equals("Test1234")))
                                {
                                    var claims = new List<Claim>
                                    {
                                        new Claim(ClaimTypes.Name,
                                            context.UserName,
                                            context.Options.ClaimsIssuer),
                                        new Claim(ClaimTypes.Role,AuthConstants.CUSTOMER_ROLE)
                                    };

                                    var ticket = new AuthenticationTicket(
                                        new ClaimsPrincipal(new ClaimsIdentity(
                                            claims,
                                            BasicAuthenticationDefaults.AuthenticationScheme)),
                                        new AuthenticationProperties(),
                                        BasicAuthenticationDefaults.AuthenticationScheme);

                                    return Task.FromResult(AuthenticateResult.Success(ticket));
                                }

                                if (context.UserName.Equals("Petrov", StringComparison.OrdinalIgnoreCase)
                                    && (context.Password.Equals("Pas$$w0rD")))
                                {
                                    var claims = new List<Claim>
                                    {
                                        new Claim(ClaimTypes.Name,
                                            context.UserName,
                                            context.Options.ClaimsIssuer),
                                        new Claim(ClaimTypes.Role,AuthConstants.ADMIN_ROLE)
                                    };

                                    var ticket = new AuthenticationTicket(
                                        new ClaimsPrincipal(new ClaimsIdentity(
                                            claims,
                                            BasicAuthenticationDefaults.AuthenticationScheme)),
                                        new AuthenticationProperties(),
                                        BasicAuthenticationDefaults.AuthenticationScheme);

                                    return Task.FromResult(AuthenticateResult.Success(ticket));
                                }

                                return Task.FromResult(AuthenticateResult.Fail("Authentication failed."));
                            }
                        };
                    });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
