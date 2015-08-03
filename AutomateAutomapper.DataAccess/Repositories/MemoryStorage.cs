using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomateAutomapper.Data.Models;

namespace AutomateAutomapper.DataAccess.Repositories
{

    public class MemoryStorage : IStorage
    {
        public IEnumerable<CustomerDao> GetAllCustomers()
        {

            yield return new CustomerDao
            {
                Id = 1,
                FirstName = "Anton",
                LastName = "Kalcik",
                Comment = "Some Comment"
            };

            yield return new CustomerDao
            {
                Id = 2,
                FirstName = "Max",
                LastName = "Mustermann",
                Comment = "Some Comment"
            };

            yield return new CustomerDao
            {
                Id = 3,
                FirstName = "Peter",
                LastName = "Sample",
                Comment = "Some Comment"
            };
        }

    }
}
