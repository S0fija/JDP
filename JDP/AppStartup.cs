﻿using JDP.Contracts.Repositories;
using JDP.Contracts.Services;
using JDP.Repositories;
using JDP.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace JDP
{
    public class AppStartup
    {
        public static IServiceProvider BuildServiceProvider()
        {
            var serviceCollection = new ServiceCollection();

            var serviceProvider =
                ConfigureServices(serviceCollection)
                    .BuildServiceProvider();

            return serviceProvider;
        }

        static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services
                .AddTransient<IExamRepository, ExamRepository>()
                .AddTransient<IStudentRepository, StudentRepository>()
                .AddTransient<IExamService, ExamService>()
                .AddTransient<IStudentService, StudentService>();

            services.AddSingleton<Func<IStudentRepository>>(x => () => x.GetService<IStudentRepository>());

            return services;
        }
    }
}