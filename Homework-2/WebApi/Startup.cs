using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Extensions;
using WebApi.Middlewares;
using WebApi.Models;
using WebApi.SwaggerOperationFilters;

namespace WebApi
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

            services.AddControllers();
            
            services.AddSwaggerGen(c =>
            {
                // Custom operationFilter for add app-version to request header
                c.OperationFilter<ApiVersionOperationFilter>();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
                c.ResolveConflictingActions(c => c.First());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
                
            }

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hi");
            //});
            //app.Map("/brands/getall", (appBuilder) =>
            //{
            //    appBuilder.Run(async (context) => {
            //        Console.WriteLine(context.Request.Method.ToString());
            //        Debug.WriteLine("/example middleware tetiklendi.");
            //        await  context.Response.WriteAsync("Hi");
            //    });
            //});
            //app.MapWhen(x => x.Request.Method == "GET", internalApp =>
            //{
            //    internalApp.Run(async context => {
            //        Console.WriteLine(context.Request.Method.ToString());
            //        await context.Response.WriteAsync("MapWhen Middleware");
                    
            //    }); 
            //});

            app.UseHttpsRedirection();

            app.UseLoggingMiddleware();

            //Custom middleware for app-version check
            app.UseVersionCheck( Options.Create(new AppVersion { Version = Configuration.GetValue<string>("AppVersion") }));

            app.UseRouting();
            
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
