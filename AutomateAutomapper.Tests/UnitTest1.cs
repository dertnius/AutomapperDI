using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.Mappers;
using AutomateAutomapper.Data.ViewModels;
using AutomateAutomapper.DataAccess;
using AutomateAutomapper.DataAccess.Repositories;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomateAutomapper.Tests
{
    [TestClass]
    public class UnitTest1
    {

        [TestInitialize]
        private IEnumerable<CustomerDomain> GetExpectedCustomers()
        {
            yield return new CustomerDomain
            {
                Id = 1,
                FirstName = "David",
                LastName = "Rivas"
            };

            yield return new CustomerDomain
            {
                Id = 2,
                FirstName = "Max",
                LastName = "H"
            };

            yield return new CustomerDomain
            {
                Id = 3,
                FirstName = "Jose",
                LastName = "SS"
            };
        }

        [TestMethod]
        public void GetAllCustomers_ManualMappingConfiguration_AllCustomersAreReturned()
        {
            //Arrange
            var configurationStore = new ConfigurationStore(new TypeMapFactory(), MapperRegistry.Mappers);
            IMappingEngine mappingEngine = new MappingEngine(configurationStore);
            IStorage storage = new MemoryStorage();
            ICustomerRepository customerRepository = new CustomerRepository(mappingEngine, storage);

            //Act
            IEnumerable<CustomerDomain> actualCustomers = customerRepository.GetAllCustomers();

            //Assert
            Mapper.AssertConfigurationIsValid();
            CollectionAssert.AreEqual(GetExpectedCustomers().ToList(), actualCustomers.ToList());
        }


        [TestMethod]
        [ExpectedException(typeof(AutoMapperMappingException))]
        public void GetAllCustomers_MissingMappingConfiguration_AutoMapperMappingException()
        {
            //Arrange
            var configurationStore = new ConfigurationStore(new TypeMapFactory(), MapperRegistry.Mappers);
            IMappingEngine mappingEngine = new MappingEngine(configurationStore);
            IStorage storage = new MemoryStorage();
            ICustomerRepository customerRepository = new CustomerRepository(mappingEngine, storage);

            //Act
            IEnumerable<CustomerDomain> actualCustomers = customerRepository.GetAllCustomers();

            //Assert
            Mapper.AssertConfigurationIsValid();
            CollectionAssert.AreEqual(GetExpectedCustomers().ToList(), actualCustomers.ToList());
        }


        [TestMethod]
        public void GetAllCustomers_UseDependencyInjectionContainer_AllCustomersAreReturned()
        {
            //Arrange
            IUnityContainer unityContainer = DependencyInjectionContainerConfiguration.InitializeContainer(new ContainerControlledLifetimeManager());
            var customerRepository = unityContainer.Resolve<ICustomerRepository>();

            //Act
            IEnumerable<CustomerDomain> actualCustomers = customerRepository.GetAllCustomers();

            //Assert
            Mapper.AssertConfigurationIsValid();
            CollectionAssert.AreEqual(GetExpectedCustomers().ToList(), actualCustomers.ToList());
        }



        public static class DependencyInjectionContainerConfiguration
        {
            public static IUnityContainer InitializeContainer(LifetimeManager autoMapperLifetimeManager = null)
            {
                IUnityContainer unityContainer = new UnityContainer();
                Bootstrapper.RegisterAutoMapperType(unityContainer);

                return unityContainer;
            }
        }


    }
}
