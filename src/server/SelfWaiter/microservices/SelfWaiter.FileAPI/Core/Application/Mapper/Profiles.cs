using AutoMapper;
using SelfWaiter.FileAPI.Core.Application.Dtos;
using SelfWaiter.FileAPI.Core.Domain;

namespace SelfWaiter.FileAPI.Core.Application.Mapper
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            #region DealerImageFile
            CreateMap<DealerImageFile, DealerImageFileDto>();
            #endregion

        }
    }
}
