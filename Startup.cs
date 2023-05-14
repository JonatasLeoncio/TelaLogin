using System;
using System.Globalization;
using System.IO;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TelaLogin.DTO;
using TelaLogin.Interfaces;
using TelaLogin.MapingDTO;
using TelaLogin.Model;
using TelaLogin.Repository;
using TelaLogin.Services;
using TelaLogin.Validation;
//using DotNetEnv;

namespace TelaLogin
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
            DotNetEnv.Env.Load();//caso use o dotenv para variaveis de ambientes

            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")               
               .Build();

            services.AddSingleton(configuration);


            // ValidatorOptions.Global.LanguageManager.Enabled = true;/desabilita aligua local edetectada
            // ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt");// muda ligua padrao flutvalidator
            /*services.AddControllers()
                .AddFluentValidation(x=>x.RegisterValidatorsFromAssemblyContaining<Startup>());*/
            services.AddControllers().AddFluentValidation();
            services.AddTransient<IValidator<UsuarioRequest>, UsuarioCreateValidator>();
            services.AddTransient<IValidator<LoginUsuario>, UsuarioLoginValidator>();
            
            services.AddAutoMapper(typeof(MapingEntitiesDto));

            services.AddSingleton<IUsuarioRepositorio, UsuarioRepository>();
            services.AddSingleton<IUsuarioService, UsuarioService>();

           

            //___________adicinando jwt e beare para token
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            //___________________________________
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TelaLogin", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TelaLogin v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication(); //acrescentar para autenticar o token

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
