using Application.Rules.Bookings.Base;
using Application.Services;
using Autofac;
using Domain.Contracts;
using Domain.Contracts.Base;
using System;
using System.Reflection;

namespace Application
{
    public static class DI
    {
        public static Assembly Assembly = Assembly.GetExecutingAssembly();

        public static ContainerBuilder RegisterApplication(this ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(Assembly)
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterType<AmountFeeService>().As<IAmountFeeService>().InstancePerDependency();
            builder.RegisterType<CurrencyConverterService>().As<ICurrencyConverterService>().InstancePerDependency();

            return builder;
        }
    }
}
