using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomateAutomapper.Data.Models;

namespace AutomateAutomapper.DataAccess
{
    public interface IStorage
    {
        IEnumerable<CustomerDao> GetAllCustomers();
    }
}
