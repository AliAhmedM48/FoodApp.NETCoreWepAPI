using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation;
using Food.App.API.Config;
using Food.App.API.Extensions;
using Food.App.API.Middlewares;
using Food.App.Core.MappingProfiles;
using Food.App.Core.Validation;
using Food.App.Core.ViewModels.Authentication;
using Food.App.Repository;
using Microsoft.EntityFrameworkCore;

namespace Food.App.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();

            #region AddSwaggerGen
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            #endregion

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(container =>
            {
                container.RegisterModule(new AutofacModule());
            });

            builder.Services.AddAutoMapper(typeof(RecipeProfile).Assembly);

            builder.Services.AddValidatorsFromAssemblyContaining<RecipeValidator>();
            builder.Services.AddCompressionServices();
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                #region UseSwagger
                app.UseSwagger();
                app.UseSwaggerUI();
                #endregion

                #region Update Database Based on Pending Migration & Data Seeding
                var appDbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
                await appDbContext.Database.MigrateAsync();

                await DataSeeder.SeedAdmins(appDbContext);
                await DataSeeder.SeedUsers(appDbContext);
                #endregion


            }

            MappingExtensions.Mapper = app.Services.GetRequiredService<IMapper>();

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseStaticFiles();


            app.MapControllers();


            #region Custom Middleware
            app.UseMiddleware<GlobalErrorHandlerMiddleware>();
            app.UseMiddleware<TransactionMiddleware>();
            #endregion


            app.Run();
        }
    }
}
