using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using LojaVirtual.Repositories;
using LojaVirtual.Repositories.Contracts;
using LojaVirtual.Libraries.Sessao;
using LojaVirtual.Libraries.Login;
using System.Net.Mail;
using System.Net;

namespace LojaVirtual
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
            //Padrão Repository
            services.AddHttpContextAccessor();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<INewsletterRepository, NewsletterRepository>();
            services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();

            //SMTP
            services.AddScoped<SmtpClient>(options=> {

                SmtpClient smtp = new SmtpClient()
                {
                    Host = Configuration.GetValue<string>("Email: ServerSMTP"),
                    Port = Configuration.GetValue<int>("Email: ServerPort"),
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Configuration.GetValue<string>("Username"), Configuration.GetValue<string>("Password")),
                    EnableSsl = true //protocolo de segurança
                };
                return smtp;
            });
            
               
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //Session - configuração
            services.AddMemoryCache(); //Guardar os dados na memoria
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(10); 
            });

            services.AddScoped<Sessao>();
            services.AddScoped<LoginCliente>();
            services.AddScoped<LoginColaborador>();

            services.AddMvc(options =>
            {
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => "O campo deve ser preenchido!");
            })
             .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
             .AddSessionStateTempDataProvider();

            services.AddSession(options =>
            {
                options.Cookie.IsEssential = true;
            });

            //caminho do banco de dados e qual conexão com ele
            string connection = "Server=RAFAEL\\SQLEXPRESS;Database=LojaVirtual;User Id=sa;Password=1234;";

            services.AddDbContext<LojaVirtualContext>(options => options.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles(); //para abrir na pagina index
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            /*
             *  https://www.site.com.br > qual controlador ? (Gestão) > Rotas
             *
             *
             *
             *
             *
             */

            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "areas",
                template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
              );

                routes.MapRoute(
                    name: "default",
                    template: "/{controller=Home}/{action=Index}/{id?}");  //configuração pagina inicial
            });
            
        }
    }
}
