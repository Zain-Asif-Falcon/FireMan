using Application.Business.Chat;
using Application.Business.Chat.IAppServices;
using Application.Interface.UnitOfWork;
using Domain.ViewModel.Options;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireManAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddDefaultPolicy(
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    //.AllowAnyOrigin()
                ));

            services.AddDbContext<FireManNewContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // chat


            //services.AddScoped<IChatBusinessServiceRepository, ChatBusinessServiceRepository>();
            //(provider =>
            //new ChatBusinessServiceRepository(provider.GetRequiredService<IChatDataServiceRepository>()));
            //services.AddTransient<IChatDataServiceRepository, ChatDataServiceRepository>();
            services.AddSignalR();
            services.AddControllers();
            services.AddMvc()
                    .AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FireManAPI", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    //Description = "JWT Authorization header with bearer scheme",
                    //Name = "Authorization",
                    //In = (ParameterLocation)1,
                    //Type = 0
                    Description = "JWT Authorization header with bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"

                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });

            var jwtSettings = new JwtSettings();

            Configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                //x.TokenValidationParameters = new TokenValidationParameters
                //{
                //    ValidateIssuerSigningKey = true,
                //    IssuerSigningKeys = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration[“JwtToken: SecretKey”])),//new SymmetricSecurityKey(key, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature),//(IEnumerable<SecurityKey>)new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),//(IEnumerable<SecurityKey>)sm,//new IEnumerable<SymmetricSecurityKey>(Encoding.ASCII.GetBytes(jwtSettings.Secret)),//(IEnumerable<SecurityKey>)(new[] { SymmetricSecurityKey(test) }),
                //    ValidateIssuer = false,
                //    ValidateAudience = false,
                //    RequireExpirationTime = false,
                //    ValidateLifetime = true
                //};
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JwtSettings:Issuer"],//Configuration["JwtToken:Issuer"],
                    ValidAudience = Configuration["JwtSettings:Issuer"],//Configuration["JwtToken:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:Secret"]))
                };

            });

            services.AddControllers(x => x.AllowEmptyInputInBodyModelBinding = true);

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //var path = Directory.GetCurrentDirectory();
            //loggerFactory..AddFile($"{path}\\Logs\\Log.txt");

            if (env.IsDevelopment() || (env.IsProduction()))
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FireManAPI v1"));
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
