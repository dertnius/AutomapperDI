using System.IO.IsolatedStorage;
using AutoMapper;
    using AutoMapper.Mappers;
using AutomateAutomapper.DataAccess;
using AutomateAutomapper.DataAccess.Repositories;
using Microsoft.Practices.Unity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

namespace AutomateAutomapper
{

    

        public static class Bootstrapper
        {

            public static void RegisterAutoMapperType(this IUnityContainer container, LifetimeManager lifetimeManager = null)
            {

                //Automate profiles
                RegisterAutomapperProfiles(container);

                //Collect all profiles in container and resolve them.
                var profiles = container.ResolveAll<Profile>();
                var autoMapperConfigurationStore = new ConfigurationStore(new TypeMapFactory(), MapperRegistry.Mappers);
                profiles.Each(autoMapperConfigurationStore.AddProfile);

                //Automapper Validate
                autoMapperConfigurationStore.AssertConfigurationIsValid();


                //Unity Injection
                container.RegisterInstance<IConfigurationProvider>(autoMapperConfigurationStore, new ContainerControlledLifetimeManager());
                container.RegisterInstance<IConfiguration>(autoMapperConfigurationStore, new ContainerControlledLifetimeManager());
                container.RegisterType<IMappingEngine, MappingEngine>(lifetimeManager ?? new TransientLifetimeManager(), new InjectionConstructor(typeof(IConfigurationProvider)));

                container.RegisterType<IStorage, MemoryStorage>();
                container.RegisterType<ICustomerRepository, CustomerRepository>();


            }

            public static void RegisterAutomapperProfiles(IUnityContainer container)
            {

                //Scanning profiles 
                var autoMapperProfileTypes = AllClasses.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                           .Where(type => type != typeof(Profile) &&
                                                  typeof(Profile).IsAssignableFrom(type));


                autoMapperProfileTypes.Each(autoMapperProfileType =>
                    container.RegisterType(typeof(Profile),
                                                  autoMapperProfileType,
                                                  autoMapperProfileType.FullName,
                                                  new ContainerControlledLifetimeManager(),
                                                  new InjectionMember[0])
                                            );




            }

        }
    }





