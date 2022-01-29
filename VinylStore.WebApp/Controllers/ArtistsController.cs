using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Application.Services;
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
    public ArtistViewModel GetById(Guid id)
    {
        var artist = _artistService.GetById(id);

        var model = _mapper.Map<ArtistViewModel>(artist);
        
        return model;
    }
    
    [HttpGet]
    [Route("GetAll")]
    public IEnumerable<ArtistViewModel> GetAll()
    {
        var artists = _artistService.GetAll();

        var models = artists.Select(artist => _mapper.Map<ArtistViewModel>(artist));
        
        return models;
    }
    
    [HttpPost]
    [Route("Create")]
    public IActionResult Create(ArtistViewModel model)
    {
        var artist = _mapper.Map<Artist>(model);
        
        _artistService.CreateArtist(artist);

        return Ok();
    }
    
    [HttpPut]
    [Route("Update")]
    public IActionResult Update(ArtistViewModel model)
    {
        var artist = _mapper.Map<Artist>(model);

        _artistService.UpdateArtist(artist);
        
        return Ok();
    }
    
    [HttpDelete]
    [Route("Delete")]
    public IActionResult Delete(Guid id)
    {
        _artistService.DeleteArtist(id);

        return Ok();
    }
}
