using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestProject.BusinessService.Account;
using TestProject.BusinessService.User;
using TestProject.Database;
using TestProject.Database.Repositories;
using TestProject.Domain.Mappings;
using TestProject.Domain.Repositories;
using TestProject.Domain.Validators;
using TestProject.WebAPI.Configuration;
using TestProject.WebAPI.ErrorHandling;
using TestProject.WebAPI.Security;

namespace TestProject.WebAPI
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

            // basic auth
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, 
                BasicAuthenticationHandler>("BasicAuthentication", null);

            // fluent validation
            services.AddMvc()
                .AddFluentValidation(
                    fv => fv.RegisterValidatorsFromAssemblyContaining<UserValidator>())
                .AddFluentValidation(
                    fv => fv.RegisterValidatorsFromAssemblyContaining<AccountValidator>()
                );

            // mem cache
            services.AddMemoryCache();

            // auto mapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserMappingProfile());
                mc.AddProfile(new AccountMappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            // db and repository
            services.AddDbContext<DatabaseContext>(options => options
                .UseSqlServer(Configuration["Database:ConnectionString"])
                .UseLazyLoadingProxies());
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();

            // services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAccountService, AccountService>();

            // swagger
            services.ConfigureSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseHsts();
            }

            // error handling middleware
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
