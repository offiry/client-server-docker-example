using Application.Contracts;
using Application.Persistance.Services;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Application.Persistance
{
    public static class DI
    {
        public static Assembly Assembly = Assembly.GetExecutingAssembly();

        public static ContainerBuilder RegisterApplicationPersistance(this ContainerBuilder builder)
        {
            builder.RegisterContext<ApplicationDbContext>();
            builder.RegisterType<ApplicationDbContextService>().As<IApplicationDbContext>().InstancePerDependency();

            return builder;
        }

        private static void RegisterContext<TContext>(this ContainerBuilder builder) where TContext : DbContext
        {
            builder
                .Register(componentContext =>
                {
                    var configuration = componentContext.Resolve<IConfiguration>();
                    var dbContextOptions = new DbContextOptions<TContext>(new Dictionary<Type, IDbContextOptionsExtension>());
                    var optionsBuilder = new DbContextOptionsBuilder<TContext>(dbContextOptions)
                        .UseSqlite(configuration.GetConnectionString("ClientConnectionString"));

                    return optionsBuilder.Options;

                }).As<DbContextOptions<TContext>>().InstancePerLifetimeScope();

            builder
                .Register(context => context.Resolve<DbContextOptions<TContext>>())
                .As<DbContextOptions>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<TContext>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}
