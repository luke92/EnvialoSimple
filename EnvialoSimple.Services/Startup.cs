using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnvialoSimple.Business.Helpers;
using EnvialoSimple.Business.Modules.AdminMail;
using EnvialoSimple.Business.Modules.Campaign;
using EnvialoSimple.Business.Modules.Checker;
using EnvialoSimple.Business.Modules.Content;
using EnvialoSimple.Business.Modules.CustomField;
using EnvialoSimple.Business.Modules.MailList;
using EnvialoSimple.Business.Modules.Member;
using EnvialoSimple.Business.Modules.Sender;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace EnvialoSimple.Services
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
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "HUA " + Configuration["AppName"] + "  API", Version = Configuration["Version"] });
                c.DescribeAllEnumsAsStrings();
            });

            services.AddScoped<BaseURI>();
            services.AddScoped<ICampaignModule, CampaignModule>();
            services.AddScoped<IMemberModule, MemberModule>();
            services.AddScoped<IMailListModule, MailListModule>();
            services.AddScoped<IContentModule, ContentModule>();
            services.AddScoped<IAdminMailModule, AdminMailModule>();
            services.AddScoped<ICustomFieldModule, CustomFieldModule>();
            services.AddScoped<ISenderModule, SenderModule>();
            services.AddScoped<ICheckerModule, CheckerModule>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(options => options
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .AllowAnyOrigin()

            );

            if (env.IsDevelopment())
            {
                // Fix problemas con proxy
                AppContext.SetSwitch("System.Net.Http.UseSocketsHttpHandler", false);
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "HUA " + Configuration["AppName"] + " API");
            });

            app.UseMvc();
        }
    }
}
