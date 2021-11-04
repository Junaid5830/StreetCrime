using StreetCrime.Configurations;
using StreetCrime.Services;
using DAL.Data;
using DAL.IRepository;
using DAL.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace StreetCrime
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
      services.AddDbContext<DatabaseContext>(options =>
      {
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("DAL"));
      });

      services.AddAuthentication();
      services.ConfigureIdentity();
      services.ConfigureJWT(Configuration);

      services.AddCors(x =>
      {
        x.AddPolicy("AllowAll", builder =>
          builder.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());
      });

      services.Configure<ApiBehaviorOptions>(options =>
      {
        options.SuppressModelStateInvalidFilter = true;
      });

      services.AddAutoMapper(typeof(MapperInitializer));

      services.AddTransient<IUnitOfWork, UnitOfWork>();
      services.AddScoped<IAuthManager, AuthManager>();

      services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "StreetCrime", Version = "v1" }); });

      services.AddControllers().AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseSwagger();
      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StreetCrime v1"));

      app.UseHttpsRedirection();

      app.UseCors("AllowAll");

      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}