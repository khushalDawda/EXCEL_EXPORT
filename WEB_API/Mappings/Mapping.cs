using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Models;
using WEB_API.Models;

namespace WEB_API.Mappings
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Account, AccountModel>().ReverseMap();
            CreateMap<Deposit, DepositModel>().ReverseMap();
            CreateMap<Cbill, CbillModel>().ReverseMap();
            CreateMap<Loan, LoanModel>().ReverseMap();
            CreateMap<ApplicationUser, UserModel>().ReverseMap();

        }
    }
}
