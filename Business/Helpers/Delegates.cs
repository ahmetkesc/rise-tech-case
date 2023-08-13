using Autofac;
using Business.Manager;
using Microsoft.Extensions.Configuration;

namespace Business.Helpers;

public class Delegates
{
    public static Action<ContainerBuilder> ContainerDelegateBuilder(IConfiguration? configuration)
    {
        if (configuration == null)
            throw new ArgumentNullException(nameof(configuration), "Configuration was not found!");

        var _delegate = new Action<ContainerBuilder>(builder =>
        {
            builder
                .RegisterType<ContactManager>().As<IContact>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ReportManager>().As<IReport>()
                .InstancePerLifetimeScope();
        });

        return _delegate;
    }
}