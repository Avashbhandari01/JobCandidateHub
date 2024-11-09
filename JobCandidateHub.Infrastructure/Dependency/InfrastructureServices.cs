using JobCandidateHub.Application.Interfaces.Repositories;
using JobCandidateHub.Application.Interfaces.Services;
using JobCandidateHub.Infrastructure.Implementation.Repositories;
using JobCandidateHub.Infrastructure.Implementation.Services;
using JobCandidateHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobCandidateHub.Infrastructure.Dependency
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString,
                    b => b.MigrationsAssembly("JobCandidateHub.Infrastructure")));

            services.AddTransient<IGenericRepository, GenericRepository>();
            services.AddScoped<ICandidateService, CandidateService>();

            return services;
        }
    }
}
