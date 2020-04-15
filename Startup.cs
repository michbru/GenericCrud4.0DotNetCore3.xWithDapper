using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using GenericCrudApiDapper.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Newtonsoft.Json.Serialization;

namespace GenericCrudApiDapper
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
      
            // Add CORS policy
            services.AddCors(options =>
            {
                options.AddPolicy("foo",
                builder =>
                {
                    // Not a permanent solution, but just trying to isolate the problem
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            services.AddControllers().AddNewtonsoftJson();
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            //services.AddMvc().AddNewtonsoftJson();

            //   .   (options => options.JsonSerializerOptions.PropertyNamingPolicy = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver());

            //services.AddMvc()
            //     .AddJsonOptions(options =>
            //     {
            //         options.JsonSerializerOptions.PropertyNamingPolicy =   SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            //     });

            //services.AddControllers()
            //      .AddJsonOptions(options =>
            //       {
            //           options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            //       });
            // keeps the casing to that of the model when serializing to json (default is converting to camelCase)
            services.AddMvc()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);
           
    //        services.AddMvc()
    //.AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy =  new System.Text.Json.JsonNamingPolicy  new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver());


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dapper Generic Crud API", Version = "v1" });
            });

           
            services.AddTransient<GenericService>();

            //services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(@"data source=s10.winhost.com;initial catalog=DB_14781_schools;persist security info=True;user id=DB_14781_schools_user;password=microwave1;MultipleActiveResultSets=True"));
           // services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(@"Data Source=(local);Initial Catalog=GenericCrudDemoDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));

            var connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dapper Generic Crud API");
            });

            app.UseHttpsRedirection();

            app.UseRouting();  // first
                               // Use the CORS policy
            app.UseCors("foo"); // second

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
