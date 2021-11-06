using HawkSoft.CodingAssessment.Data;
using HawkSoft.CodingAssessment.Data.Repositories;
using HawkSoft.CodingAssessment.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HawkSoft.CodingAssessment
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddCodingAssessmentServices(this IServiceCollection services)
        {
            services.AddScoped<IUserContactMongoDataContext, UserContactMongoDataContext>();
            services.AddScoped<IBusinessContactRepository, BusinessContactRepository>();
            services.AddScoped<IBusinessContactService, BusinessContactService>();
            return services;;
        }
    }
}