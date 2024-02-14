using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchableDesign.Repository.Repository.AccountRepository;
using SearchableDesign.Repository.Repository.EmployeeRepository;
using SearchableDesign.Repository.Repository.ManageEmployees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SearchableDesign.Repository.IOC
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddSingleton<IConfiguration>(configuration);
            services.AddScoped<IAccountRepository, AccountRepository>(); 
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IManageEmployeeRepository, ManageEmployeeRepository>();
            //to set session
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }
    }
}
