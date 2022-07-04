using Autofac;
using Domain.Contracts;
using Infrastructure.Services;
using System.Reflection;

namespace Infrastructure
{
    public static class DI
    {
        public static Assembly Assembly = Assembly.GetExecutingAssembly();

        public static ContainerBuilder RegisterInfrastructure(this ContainerBuilder builder)
        {
            builder
               .RegisterType<HttpRequests>()
               .As<IHttpRequests>()
               .SingleInstance();

            return builder;
        }
    }
}
