using AutoMapper;
using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.CityCommands;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.CountryCommands;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.DealerCommands;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.DistrictCommands;
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
            CreateMap<UpdateCityCommand, City>();
            CreateMap<CreateRangeCityRequest, City>();

            #endregion

            #region Country

            CreateMap<Country, CountryDto>();
            CreateMap<CreateCountryCommand, Country>();
            CreateMap<UpdateCountryCommand, Country>();
            CreateMap<CreateRangeCountryRequest, Country>();

            #endregion

            #region Dealer

            CreateMap<Dealer, DealerDto>();
            CreateMap<CreateDealerCommand, Dealer>();

            #endregion


            #region District

            CreateMap<District, DistrictDto>();
            CreateMap<CreateDistrictCommand, District>();
            CreateMap<CreateRangeDistrictRequest, District>();

            #endregion


            #region UserProfile

            CreateMap<UserProfile, UserProfileDto>();

            #endregion

        }
    }
}
