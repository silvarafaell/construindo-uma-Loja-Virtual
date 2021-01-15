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
using LojaVirtual.Libraries.Email;
using LojaVirtual.Libraries.Middleware;
using LojaVirtual.Libraries.CarrinhoCompra;
using AutoMapper;
using LojaVirtual.Libraries.AutoMapper;
using LojaVirtual.Libraries.Gerenciador.Frete;
using WSCorreios;

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
            //AutoMapper
            services.AddAutoMapper(config=>config.AddProfile<MappingProfile>());

            //Padrão Repository
            services.AddHttpContextAccessor();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<INewsletterRepository, NewsletterRepository>();
            services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IImagemRepository, ImagemRepository>();
            //SMTP
            services.AddScoped<SmtpClient>(options =>
            {

                SmtpClient smtp = new SmtpClient()
                {
                    Host = Configuration.GetValue<string>("Email:ServerSMTP"),
                    Port = Configuration.GetValue<int>("Email:ServerPort"),
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Configuration.GetValue<string>("Email:Username"), Configuration.GetValue<string>("Email:Password")),
                    EnableSsl = true
                };
                return smtp;
            });

            services.AddScoped<CalcPrecoPrazoWSSoap>(options => {
                var servico = new CalcPrecoPrazoWSSoapClient(CalcPrecoPrazoWSSoapClient.EndpointConfiguration.CalcPrecoPrazoWSSoap);
                return servico;
            });
            services.AddScoped<GerenciarEmail>();
            services.AddScoped<LojaVirtual.Libraries.Cookie.Cookie>();
            services.AddScoped<CarrinhoCompra>();
            services.AddScoped<CalcularPacote>();
            services.AddScoped<WSCorreiosCalcularFrete>();


            services.Configure<CookiePolicyOptions>(options =>
            {              
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //Session - configuração
            services.AddMemoryCache(); //Guardar os dados na memoria
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
            });

            services.AddScoped<Sessao>();
            services.AddScoped<LojaVirtual.Libraries.Cookie.Cookie>();
            services.AddScoped<LoginCliente>();
            services.AddScoped<LoginColaborador>();

            services.AddMvc(options =>
            {
                options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => "O valor {0} não é válido para {1}.");
                options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x => "Não foi fornecido um valor para o campo {0}.");
                options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "Campo obrigatório.");
                options.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "É necessário que o body na requisição não esteja vazio.");
                options.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor((x) => "O valor {0} não é válido.");
                options.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "O valor fornecido é inválido.");
                options.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "O campo deve ser um número.");
                options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor((x) => "O valor fornecido é inválido para {0}.");
                options.ModelBindingMessageProvider.SetValueIsInvalidAccessor((x) => "O valor fornecido é inválido para {0}.");
                options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x => "O campo {0} deve ser um número.");
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => "O valor nulo é inválido.");
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
            app.UseMiddleware<ValidateAntiForgeryTokenMiddleware>();
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
