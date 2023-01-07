using Core.Application.Pipelines.Validation;
using FluentValidation;
using Kodlama.io.Devs2.Application.Features.Auths.Rules;
using Kodlama.io.Devs2.Application.Features.OperationClaims.Rules;
using Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Rules;
using Kodlama.io.Devs2.Application.Features.Technologies.Rules;
using Kodlama.io.Devs2.Application.Services.AuthService;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs2.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Application Katmanı ile ilgli bütün injectionlarımızı yaptığımız yer

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            #region İş Kurallarının Servisleri
            services.AddScoped<ProgrammingLanguageRules>(); // Business Kuralları bir kere bellekte durur.
            services.AddScoped<TechnologyRules>();
            services.AddScoped<AuthBusinessRules>();
            services.AddScoped<OperationClaimRules>();
            #endregion


            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); // Fluent Validation: Bir nesnenin özelliklerinin iş kurallarına dahil etmek için format uygunluğu ile ilgili
            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>)); // Rol Bazlı Yetkilendirme
            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>)); // Ön Belleğe atma
            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheRemovingBehavior<,>)); // Ön belleği temizleme
            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>)); // Loglama  

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            #region Service - Çoğu yerde kullanılacak metotları yazdığımız sınıfları Bağlıyoruz
            services.AddScoped<IAuthService, AuthManager>();
            #endregion

            return services;


        }
    }
}
