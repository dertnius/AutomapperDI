using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  AutomateAutomapper.Data.Models;
using AutomateAutomapper.Data.ViewModels;

namespace AutomateAutomapper.DataAccess
{
    public interface ICustomerRepository
    {
        
            IEnumerable<CustomerDomain> GetAllCustomers();
        

    }
}
