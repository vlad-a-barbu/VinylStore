using AutoMapper;
using VinylStore.Web.ViewModels;

namespace VinylStore.Web.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<GenreViewModel, DataObjects.Genre>();
        CreateMap<DataObjects.Genre, GenreViewModel>();

        CreateMap<UserViewModel, DataObjects.User>();
        CreateMap<DataObjects.User, UserViewModel>();
    }
}
