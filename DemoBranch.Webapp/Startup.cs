using DemoBranch.Webapp.Persistence.DataAksess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using DemoBranch.Webapp.Appliction.Contracts;
using DemoBranch.Webapp.Persistence.DataAksess.Repositories;
using DemoBranch.Webapp.Appliction.Person.Commands.Create;
using DemoBranch.Webapp.Appliction.Person.Commands.Change;
using DemoBranch.Webapp.Appliction.Person.Queries;
using AutoMapper;
using DemoBranch.Webapp.Appliction.AutoMapperProfile;
using MediatR;

namespace DemoBranch.Webapp
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DemoBranch.Webapp", Version = "v1" });
                c.SchemaFilter<CreateEventExamples>();
                c.SchemaFilter<ChangeEventExamples>();
          

            });

            services.AddDbContext<DemoEventContext>(options =>
              options.UseInMemoryDatabase("Test"));


            services.AddScoped<IDemoEventRepository,DemoEventRepository>();
            services.AddScoped<IDemoEventQuery, DemoEventQuery>();
       
            services.AddScoped<CreatePersonHandler>();
            services.AddScoped<ChangePersonHandler>();
            services.AddScoped<GetAllPersonHandler>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EventProfile>();
            });
            var mapper = new Mapper(mapperConfig);
            services.AddSingleton<IMapper>(mapper);

            services.AddMediatR(typeof(CreatePersonHandler).Assembly);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DemoBranch.Webapp v1"));
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
