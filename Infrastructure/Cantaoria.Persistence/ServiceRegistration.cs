using AvvaMobile.Core.Utilities.Mail;
using Cantaoria.Application.Repositories;
using Cantaoria.Persistence.Interfaces;
using Cantaoria.Persistence.Repositories;
using Cantaoria.Persistence.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cantaoria.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {


            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            services.AddScoped<IUserReadRepository, UserReadRepository>();
            services.AddScoped<IUserWriteRepository, UserWriteRepository>();

            services.AddScoped<IRoleReadRepository, RoleReadRepository>();
            services.AddScoped<IRoleWriteRepository, RoleWriteRepository>();

            services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();

            services.AddScoped<IMenuReadRepository , MenuReadRepository>();
            services.AddScoped<IMenuWriteRepository, MenuWriteRepository>();

            services.AddScoped<IPermissionReadRepository, PermissionReadRepository>();
            services.AddScoped<IPermissionWriteRepository, PermissionWriteRepository>();

            //services.AddScoped<IMailService, MailManager>();
            //services.AddScoped<AvvaMobile.Core.Caching.AppSettingsKeys>();

            services.AddScoped<ILoginService, LoginService>();


        }
    }
}
