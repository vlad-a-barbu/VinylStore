using VinylStore.DataAccess;
using VinylStore.DataObjects.Entities;
using VinylStore.Domain.Base;
using VinylStore.Domain.Mapper;
using EFModels = VinylStore.DataAccess.EF.Models;

namespace VinylStore.Domain.Services;

public class AddressDomainService : IDomainService<Address>
{
    private readonly UnitOfWork _uow;

    public AddressDomainService(UnitOfWork uow)
    {
        _uow = uow;
    }

    public Address? Get(Guid id)
    {
        var address = _uow.Addresses.Get(id);

        return address is null ? null :
            Builder
                .For<EFModels.Address, Address>()
                .Invoke(address);
    }

    public IEnumerable<Address> GetAll(Func<Address, bool>? filter = null)
    {
        var addresses = _uow.Addresses
            .GetAll()
            .ToList()
            .Select(address => 
                Builder
                    .For<EFModels.Address, Address>()
                    .Invoke(address)
            );

        return filter is null ? addresses : addresses.Where(filter);
    }
    
    public Guid Create(Address address)
    {
        var entity = Builder
            .For<Address, EFModels.Address>()
            .Invoke(address);
        
        _uow.Addresses.Insert(entity);
        _uow.SaveChanges();

        return entity.Id;
    }

    public void Update(Address address)
    {
        var entity = _uow.Addresses.Get(address.Id)
            ?? throw new ArgumentException($"Address {address.Id} not found", nameof(address));
        
        _uow.Addresses.Update(
            Builder
                .For<Address, EFModels.Address>(entity)
                .Invoke(address)
        );
        
        _uow.SaveChanges();
    }

    public void Delete(Guid id)
    {
        _uow.Addresses.Delete(id);
        
        _uow.SaveChanges();
    }
}
