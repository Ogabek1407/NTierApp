
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NTierApplication.Infrastructure;
using NTierApplication.Infrastructure.Repository;
using NTierApplication.Infrastructure.Repository.Interface;
using NTierApplication.Servece.Service;
using NTierApplication.Servece.Service.Interface;
using NTierApplication.Web.ActionHelpers;
using System.Text;

namespace NTierApplication.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var secretKey = Encoding.ASCII.GetBytes(NTierAppliction.Domain.Models.SecurityKey.key);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                };
            });

            // Add services to the container.

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
            builder.Services.AddDbContext<MianContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            builder.Services.AddScoped<IItemService,ItemService>();
            builder.Services.AddScoped<IItemRepository,ItemRepository>();
            builder.Services.AddScoped<IUserRepository,UserRepository>();
            builder.Services.AddScoped<IUserService,UserService>();
            builder.Services.AddScoped<ITokenService,TokenService>();

            builder.Services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Enter Like this => Bearer token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer",
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.MapControllers();
            app.UseAuthentication();
            app.UseAuthorization();
            app.Run();
        }
    }
}
