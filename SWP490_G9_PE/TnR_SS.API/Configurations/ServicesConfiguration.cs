using Microsoft.Extensions.DependencyInjection;
using TnR_SS.DataEFCore.Repositories;
using TnR_SS.DataEFCore.UnitOfWorks;
using TnR_SS.Domain.Repositories;
using TnR_SS.Domain.Supervisor;
using TnR_SS.Domain.UnitOfWork;

namespace TnR_SS.API.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOTPRepository, OTPRepository>()
                .AddScoped<IRoleUserRepository, RoleUserRepository>()
                .AddScoped<IUserInforRepository, UserInforRepository>()
                .AddScoped<IBasketRepository, BasketRepository>()
                .AddScoped<IFishTypeRepository, FishTypeRepository>()
                .AddScoped<IPondOwnerRepository, PondOwnerRepository>()
                .AddScoped<IPurchaseDetailRepository, PurchaseDetailRepository>()
                .AddScoped<IPurchaseRepository, PurchaseRepository>()
                .AddScoped<IEmployeeRepository, EmployeeRepository>()
                .AddScoped<IHistorySalaryEmpRepository, HistorySalaryEmpRepository>();

            return services;
        }

        public static IServiceCollection ConfigureSupervisor(this IServiceCollection services)
        {
            services.AddScoped<ITnR_SSSupervisor, TnR_SSSupervisor>();

            return services;
        }

        public static IServiceCollection ConfigureUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
