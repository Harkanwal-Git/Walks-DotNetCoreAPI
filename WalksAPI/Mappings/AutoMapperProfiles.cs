using AutoMapper;
using Walks.API.Models.Domain;
using Walks.API.Models.DTO;

namespace Walks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //CreateMap<UserDTO,UserDomain>().ForMember(dest=>dest.Name,opt=>opt.MapFrom(source=>source.FullName)).ReverseMap();

            CreateMap<RegionDto,Region>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();

            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<WalkDto, Walk>().ReverseMap();
            CreateMap<DifficultyDto, Difficulty>().ReverseMap();
            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
        }


    }

    public class UserDTO
    {
        public string FullName { get; set; }
    }
    public class UserDomain
    {
        public string Name { get; set; }
    }
}
