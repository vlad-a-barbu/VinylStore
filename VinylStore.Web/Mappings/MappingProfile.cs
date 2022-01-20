using AutoMapper;
using VinylStore.DataObjects.AuthenticationModels;
using VinylStore.DataObjects.BusinessModels;
using VinylStore.DataObjects.Entities;
using VinylStore.Web.ViewModels;
using User = VinylStore.DataAccess.EF.Models.User;

namespace VinylStore.Web.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<GenreViewModel, Genre>();
        CreateMap<Genre, GenreViewModel>();

        CreateMap<UserViewModel, CompleteUser>();
        CreateMap<CompleteUser, UserViewModel>();

        CreateMap<RegisterClientViewModel, RegisterUser>();
        CreateMap<RegisterAdminViewModel, RegisterUser>();

        CreateMap<LoginViewModel, LoginUser>();
        CreateMap<LoginUser, LoginViewModel>();

        CreateMap<User, AuthenticatedUser>();
    }
}
