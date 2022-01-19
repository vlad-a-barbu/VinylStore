using VinylStore.Domain.Services;

namespace VinylStore.Domain;

public class DomainServices
{
    public DomainServices(
        Lazy<GenreDomainService> genreDomainService,
        Lazy<UserDomainService> userDomainService
    )
    {
        _genreDomainService = genreDomainService;
        _userDomainService = userDomainService;
    }

    private readonly Lazy<GenreDomainService> _genreDomainService;
    public GenreDomainService Genre => _genreDomainService.Value;

    private readonly Lazy<UserDomainService> _userDomainService;
    public UserDomainService User => _userDomainService.Value;
}
