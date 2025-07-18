﻿using FluentValidation;
using MediatR;
using System.Reflection;
using TodoList.Core.Validation;

namespace ToDoList.WebApi.DependencyInjection;

public static class MediatorInjection
{
    public static void AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("ToDoList.Application")));

        const string applicationAssemblyName = "ToDoList.Application";
        var assembly = AppDomain.CurrentDomain.Load(applicationAssemblyName);

        AssemblyScanner
            .FindValidatorsInAssembly(assembly)
            .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}
