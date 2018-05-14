using Autofac;
using Autofac.Integration.Mvc;

using PersonApplication.DataAccess;
using PersonApplication.DataAccess.Repository;
using System.Data.Entity;
using System.Web.Mvc;

namespace PersonApplication.AutofacWeb
{
    public  class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType(typeof(PersonDBContext)).As<DbContext>();
            builder.RegisterType(typeof(PersonRepository)).As<IPersonRepository>();
            builder.RegisterType(typeof(AddressRepository)).As<IAddressRepository>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}