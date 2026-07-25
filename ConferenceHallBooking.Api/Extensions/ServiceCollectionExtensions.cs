using AutoMapper;
using ConferenceHallBooking.Application.Abstractions.IRepository;
using ConferenceHallBooking.Application.Abstractions.IServices;
using ConferenceHallBooking.Application.Abstractions.Other;
using ConferenceHallBooking.Application.Helpers;
using ConferenceHallBooking.Application.Mapping;
using ConferenceHallBooking.Application.Services;
using ConferenceHallBooking.Application.Strategies;
using ConferenceHallBooking.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.OpenApi;

namespace ConferenceHallBooking.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            //Repositories
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IConferenceHallRepository, ConferenceHallRepository>();

            //Services
            services.AddScoped<IConferenceHallService, ConferenceHallService>();
            services.AddScoped<IBookingService, BookingService>();

            //Other
            services.AddScoped<IBookingPricingCalculator, BookingPricingCalculator>();
            services.AddScoped<IPricingStrategy, StandardHoursPricingStrategy>();
            services.AddScoped<IPricingStrategy, PeakHoursSurchargePricingStrategy>();
            services.AddScoped<IPricingStrategy, MorningDiscountPricingStrategy>();
            services.AddScoped<IPricingStrategy, EveningDiscountPricingStrategy>();

            return services;
        }

        public static IServiceCollection AddAutomapper(this IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<CreateConferenceHallProfile>();
                    cfg.AddProfile<ConferenceHallResponseProfile>();
                    cfg.AddProfile<CreateBookingProfile>();
                    cfg.AddProfile<BookingResponseProfile>();

                    cfg.AllowNullCollections = true;
                }, loggerFactory);

                config.AssertConfigurationIsValid();
                return config.CreateMapper();
            });

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateConferenceHallProfile>();
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Conference Hall Booking API",
                    Version = "v1",
                    Description = "Conference Hall Booking API — venue search, reservations, and price calculation."
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter bearer token"
                });

                options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Bearer", document)] = []
                });
            });

            return services;
        }
    }
}