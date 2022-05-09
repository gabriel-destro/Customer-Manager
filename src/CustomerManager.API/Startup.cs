using CustomerManager.Application;
using CustomerManager.Persistence.Contextos;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using CustomerManager.Swagger;
using CustomerManager.Application.Contratos;
using CustomerManager.Persistence;
using CustomerManager.Persistence.Contratos;
using FluentValidation;
using CustomerManager.Domain.Models;
using CustomerManager.Domain.Validators;

namespace CustomerManager
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
            services.AddDbContext<ClientContext>(
                context => context.UseSqlite(Configuration.GetConnectionString("Default"))
            );
            
            services.AddControllers()
                .AddFluentValidation(x => x
                    .RegisterValidatorsFromAssemblyContaining<Startup>());
                       
            services.AddTransient<IValidator<Client>, CreateClientValidator>();
                
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CustomerManager", Version = "v1" });
                c.SchemaFilter<SwaggerIgnoreFilter>();
            });
            
            /* DI */
            // Service
            services.AddScoped<IClientService, ClientService>()   ;

            // Persist
            services.AddScoped<IClientPersist, ClientPersist>()   ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CustomerManager v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
