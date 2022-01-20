using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Application.Services;
using VinylStore.DataAccess.EF.Models;
using VinylStore.Web.Validation.Attributes;
using VinylStore.Web.ViewModels;
using Artist = VinylStore.DataObjects.Entities.Artist;

namespace VinylStore.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ArtistsController : ControllerBase
{
    private readonly ArtistService _artistService;
    private readonly IMapper _mapper;

    public ArtistsController(
        ArtistService artistService,
        IMapper mapper
    )
    {
        _artistService = artistService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [Route("Get/{id}")]
    [Authorization(Role.Admin, Role.Client)]
    public ArtistViewModel GetById(Guid id)
    {
        var artist = _artistService.GetById(id);

        var model = _mapper.Map<ArtistViewModel>(artist);
        
        return model;
    }
    
    [HttpGet]
    [Route("GetAll")]
    [Authorization(Role.Admin, Role.Client)]
    public IEnumerable<ArtistViewModel> GetAll()
    {
        var artists = _artistService.GetAll();

        var models = artists.Select(artist => _mapper.Map<ArtistViewModel>(artist));
        
        return models;
    }
    
    [HttpPost]
    [Route("Create")]
    [Authorization(Role.Admin)]
    public IActionResult Create(ArtistViewModel model)
    {
        var artist = _mapper.Map<Artist>(model);
        
        _artistService.CreateArtist(artist);

        return Ok();
    }
    
    [HttpPut]
    [Route("Update")]
    [Authorization(Role.Admin)]
    public IActionResult Update(ArtistViewModel model)
    {
        var artist = _mapper.Map<Artist>(model);

        _artistService.UpdateArtist(artist);
        
        return Ok();
    }
    
    [HttpDelete]
    [Route("Delete")]
    [Authorization(Role.Admin)]
    public IActionResult Delete(Guid id)
    {
        _artistService.DeleteArtist(id);

        return Ok();
    }
}
