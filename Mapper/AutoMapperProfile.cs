using AutoMapper;
using MiniBankApi.Dto;
using MiniBankApi.models;

namespace MiniBankApi.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterNewAccountModel, Account>();
            CreateMap<UpdateAccountModel, Account>();
            CreateMap<Account, GetAccountModel>();
        }
    }
}