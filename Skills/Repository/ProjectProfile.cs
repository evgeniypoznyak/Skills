using AutoMapper;
using Skills.Entity;
using Skills.ValueObject;
using Skills.Adapter;

namespace Skills.Repository
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectDto, Project>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(
                        src => new ObjectId(src.Id)
                    )
                )
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(
                        src => new ObjectName(src.Name)
                    )
                )
//                .ForMember(dest => dest.Slug,
//                    opt => opt.MapFrom(
//                        src => new Slug(src)
//                    )
//                )
                ;
        }
    }
}