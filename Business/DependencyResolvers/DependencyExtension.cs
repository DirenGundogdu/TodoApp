using AutoMapper;
using Business.Interfaces;
using Business.Mappings.AutoMapper;
using Business.Services;
using Business.ValidationRules;
using DataAccess.Context;
using DataAccess.UnitofWork;
using Dtos.WorkDtos;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Business.DependencyResolvers
{
    public static class DependencyExtension
    {

        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddDbContext<TodoContext>(opt =>
            {
                opt.UseSqlServer("server=servername; database=TodoDb; integrated security=true;");
                opt.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
            });

            var configuration = new MapperConfiguration(opt => {
                opt.AddProfile(new WorkProfile());
            });

            var mapper = configuration.CreateMapper();

            services.AddSingleton(mapper);

            services.AddScoped<IUow, Uow>();
            services.AddScoped<IWorkService,WorkService>();
            services.AddTransient<IValidator<WorkCreateDto>, WorkCreateDtoValidator>();
            services.AddTransient<IValidator<WorkUpdateDto>, WorkUpdateDtoValidator>();

        }
    }
}
