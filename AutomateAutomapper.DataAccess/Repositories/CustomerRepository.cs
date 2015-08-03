using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomateAutomapper.Data.Models;
using AutomateAutomapper.Data.ViewModels;


namespace AutomateAutomapper.DataAccess.Repositories
{
    public class CustomerRepository: ICustomerRepository
    {

        private readonly IMappingEngine _mappingEngine;
        private readonly IStorage _storage;

        public CustomerRepository(IMappingEngine mappingEngine, IStorage storage)
        {
            if (mappingEngine == null)
            {
                throw new ArgumentNullException("mappingEngine");
            }
            if (storage == null)
            {
                throw new ArgumentNullException("storage");
            }
            _mappingEngine = mappingEngine;
            _storage = storage;
        }

        public IEnumerable<CustomerDomain> GetAllCustomers()
        {
            IEnumerable<CustomerDao> customerEntities = _storage.GetAllCustomers();
            return _mappingEngine.Map<IEnumerable<CustomerDomain>>(customerEntities);
        }

    }
}
