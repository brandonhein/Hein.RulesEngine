using Amazon;
using Hein.Framework.DependencyInjection;
using Hein.Framework.Dynamo;
using Hein.RulesEngine.Framework.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Hein.RulesEngine.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile($"appsettings.json", optional: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddMvc(options =>
            {
                options.InputFormatters.Add(new XmlSerializerInputFormatter(options));
                options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            });

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "Hein.RulesEngine.API" });
                x.DescribeAllEnumsAsStrings();
            });

            //TODO put this into appsettings.json
            var logConfig = new LogConfiguration()
            {
                SystemName = "Hein.RulesEngine.API",
                Environment = "Test",
                EnabledLevels = new string[] { "Debug", "Info", "Warn", "Error" }
            };
            services.AddSingleton<ILogProvider>(s => new ConsoleLogProvider(logConfig));


            services.AddTransient<IRepositoryContext>(s => new RepositoryContext(RegionEndpoint.USEast2));

            services.BuildServiceLocator();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                if (env.IsDevelopment())
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hein.RulesEngine.API");
                }
                else
                {
                    c.SwaggerEndpoint("/Prod/swagger/v1/swagger.json", "Hein.RulesEngine.API");
                }
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
