using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MyDishesApp.Service.Dtos.Auth;
using MyDishesApp.Service.Extensions;
using MyDishesApp.WebApi.Authorization;
using MyDishesApp.WebApi.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Text;

namespace MyDishesApp.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var loginSettings = new LoginSettings
            {
                Audience = _configuration["Jwt:Audience"],
                Issuer = _configuration["Jwt:Issuer"],
                SecretKey = _configuration["Jwt:SecretKey"]
            };

            services.AddSingleton(loginSettings);

            services.AddControllers();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = loginSettings.Issuer,
                        ValidAudience = loginSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(loginSettings.SecretKey)),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                config.AddPolicy(Policies.User, Policies.UserPolicy());
            });

            services.AddMvc(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
                options.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
            });

            // Configure CORS so the API allows requests from JavaScript.  
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOriginsHeadersAndMethods",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            // Register automapper and the services
            services.AddAssembliesToAutoMapper();
            services.AddServiceLayerWithDependencies(_configuration["ConnectionStrings:MyDishesAppDB"]);
            services.AddSwagger(_configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/webapi/swagger.json", "WebApi");
                    c.RoutePrefix = string.Empty;
                });
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    });
                });
            }

            app.UseRouting();
            app.UseCors("AllowAllOriginsHeadersAndMethods");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}