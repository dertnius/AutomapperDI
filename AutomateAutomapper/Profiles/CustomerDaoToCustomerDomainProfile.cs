using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutomateAutomapper.Data.Models;
using AutomateAutomapper.Data.ViewModels;


namespace AutomateAutomapper.Profiles
{

    public class CustomerDaoToCustomerDomainProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<CustomerDomain, CustomerDao>()
                .ForMember(customerEntity => customerEntity.Comment, opt => opt.Ignore());
        }
    }
}
