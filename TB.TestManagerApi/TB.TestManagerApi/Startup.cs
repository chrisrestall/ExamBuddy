using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
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
            
        public void ConfigureServices(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddTransient<IExamMasterCommandService, ExamMasterCommandService>();
            services.AddTransient<IExamTypeMetaCommandService, ExamTypeMetaCommandService>();
            services.AddTransient<IExamTypeMetaQueryService, ExamTypeMetaQueryService>();
            services.AddTransient<IExamMasterQueryService, ExamMasterQueryService>();
            services.AddTransient<IExamQuestionQueryService, ExamQuestionQueryService>();            
            services.AddTransient<IExamTypeMetaManager, ExamTypeMetaManager>();
            services.AddTransient<IExamStructureManager, ExamStructureManager>();
            services.AddTransient<IExamTypeMetaQueries, ExamTypeMetaQueries>();
            services.AddTransient<IExamTypeMetaCommands, ExamTypeMetaCommands>();
            services.AddTransient<IExamStructureQueries, ExamStructureQueries>();
            services.AddTransient<IExamStructureCommands, ExamStructureCommands>();
            services.AddTransient<IExamQuestionCommandService, ExamQuestionCommandService>();
            services.AddTransient<ISqlServerConnectionProvider, SqlServerConnectionProvider>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(_apiVersion, new OpenApiInfo { Title = _apiName, Version = _apiVersion });
            });
            services.AddMemoryCache();
        }

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
