using Business.Abstract;
using Business.Concrete;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
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
            //Autofac, Ninject, CastleWindsor, StrucutreMap, LightInject, DryInject --> IoC Container işini aşağıdaki service yerine yapıyor.
            //AOP -- Bütün methotları loglama istedik, bir metodun önünde veya sonunda bir hata verdiğinde oluşturulan mimaridir. method başına konulan attribute [LogAspect] örneği gibi.
            services.AddControllers();
            //Aşağıdaki servis -> biri constructor'da IProductService isterse ona arka planda bir tane ProductManager new'i ver demek
            //IProductService şeklinde bir bağımlılık görürsen ProductManager onun karşılığıdır demek. IoC Container (İçerisinde data tutmadığımız classlar için kullanılır AddSingleton.Örn; Sepet datasını ProductManager içerisinde tutuyorsak ve AddSingleton kullanırsak farklı customer'ların sepetleri birbirine girer.)

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddCors(); //Cross Origin Response

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });

            services.AddDependencyResolvers(new ICoreModule[] {
                new CoreModule()
            });

            //ServiceTool.Create(services);

            //services.AddSingleton<IProductService,ProductManager>();
            //services.AddSingleton<IProductDal, EfProductDal>(); //ProductManager constructor'ı IProductDal bağımlılığına maruz kaldığı için referans olarak EfProductDal'ı verdik ve hata vermesini önledik.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureCustomExceptionMiddleware();

            app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader()); // Belirtilen adresten herhangi bir istek için izin ver

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
