using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using Domain;
using Application;
using Application.Persistance;
using Infrastructure;
using Microsoft.Extensions.Configuration;

namespace Bootstrapper
{
    public static class ApplicationServices
    {
        public static Assembly Assembly = Assembly.GetExecutingAssembly();

        public static IServiceProvider RegisterAutofac(this IServiceCollection services)
        {
            return new AutofacServiceProvider(ContainerBuilder(services));
        }

        public static IContainer ContainerBuilder(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            var assemblyList = new List<Assembly>
            {
                Assembly,
                Application.DI.Assembly,
                Domain.DI.Assembly,
                Infrastructure.DI.Assembly,
            };

            builder
                .RegisterApplication()
                .RegisterDomain(assemblyList)
                .RegisterInfrastructure()
                .RegisterApplicationPersistance();

            builder.Populate(services);

            var container = builder.Build();
            return container;
        }
    }
}
