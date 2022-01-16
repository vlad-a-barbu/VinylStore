using AutoMapper;
using VinylStore.DataObjects;
using VinylStore.Web.ViewModels;

namespace VinylStore.Web.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateGenre, Genre>();
    }
}
