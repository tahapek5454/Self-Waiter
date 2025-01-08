using AutoMapper;
using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.Shared.Core.Domain.Dtos;
using SelfWaiter.Shared.Core.Domain.Entities;

namespace SelfWaiter.DealerAPI.Core.Application.Mapper
{
    public class Profiles: Profile
    {
        public Profiles()
        {
            #region City

            CreateMap<City, CityDto>();

            #endregion

            #region Country

            CreateMap<Country, CountryDto>();

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

            #region Base
            CreateMap<IEntity, IDto>().ReverseMap();
            #endregion
        }
    }
}
