using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TB.TestManagerApi.Repository;
using TB.TestManagerApi.Services;
using TB.TestManagerApi.Providers;
using TB.TestManagerApi.Mapping;
using AutoMapper;

namespace TB.TestManagerApi
{
    public class Startup
    {
        private readonly string _apiName = "Exam Manager API";
        private readonly string _apiVersion = "v1";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddTransient<IExamTypeMetaCommandService, ExamTypeMetaCommandService>();
            services.AddTransient<IExamTypeMetaQueryService, ExamTypeMetaQueryService>();
            services.AddTransient<IExamMasterQueryService, ExamMasterQueryService>();
            services.AddTransient<IExamQuestionQueryService, ExamQuestionQueryService>();            
            services.AddTransient<IExamTypeMetaManager, ExamTypeMetaManager>();
            services.AddTransient<IExamStructureManager, ExamStructureManager>();
            services.AddTransient<IExamTypeMetaQueries, ExamTypeMetaQueries>();
            services.AddTransient<IExamTypeMetaCommands, ExamTypeMetaCommands>();
            services.AddTransient<IExamStructureQueries, ExamStructureQueries>();    
            services.AddTransient<ISqlServerConnectionProvider, SqlServerConnectionProvider>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(_apiVersion, new OpenApiInfo { Title = _apiName, Version = _apiVersion });
            });
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{_apiName}, {_apiVersion}"));
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
