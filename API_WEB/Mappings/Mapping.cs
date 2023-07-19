using API_WEB.DataBaseModels;
using AutoMapper;
using ViewModels.Models;

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
