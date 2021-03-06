using VinylStore.Domain.Services;

namespace VinylStore.Domain;

public class DomainServices
{
    public DomainServices(
        Lazy<GenreDomainService> genreDomainService,
        Lazy<UserDomainService> userDomainService,
        Lazy<AddressDomainService> addressDomainService,
        Lazy<ArtistDomainService> artistDomainService,
        Lazy<AlbumDomainService> albumDomainService
    )
    {
        _genreDomainService = genreDomainService;
        _userDomainService = userDomainService;
        _addressDomainService = addressDomainService;
        _artistDomainService = artistDomainService;
        _albumDomainService = albumDomainService;
    }

    private readonly Lazy<GenreDomainService> _genreDomainService;
    public GenreDomainService Genre => _genreDomainService.Value;

    private readonly Lazy<UserDomainService> _userDomainService;
    public UserDomainService User => _userDomainService.Value;

    private readonly Lazy<AddressDomainService> _addressDomainService;
    public AddressDomainService Address => _addressDomainService.Value;

    private readonly Lazy<ArtistDomainService> _artistDomainService;
    public ArtistDomainService Artist => _artistDomainService.Value;

    private readonly Lazy<AlbumDomainService> _albumDomainService;
    public AlbumDomainService Album => _albumDomainService.Value;
}

