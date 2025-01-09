using AutoMapper;
using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.CityCommands;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.CountryCommands;
using SelfWaiter.DealerAPI.Core.Domain.Entities;

namespace SelfWaiter.DealerAPI.Core.Application.Mapper
{
    public class Profiles: Profile
    {
        public Profiles()
        {
            #region City

            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<CreateCityCommand, City>();

            #endregion

            #region Country

            CreateMap<Country, CountryDto>();
            CreateMap<CreateCountryCommand, Country>();

            #endregion

            #region Dealer

            CreateMap<Dealer, DealerDto>();

            #endregion


            #region District

            CreateMap<District, DistrictDto>();

            #endregion


            #region UserProfile

            CreateMap<UserProfile, UserProfileDto>();

            #endregion

        }
    }
}
