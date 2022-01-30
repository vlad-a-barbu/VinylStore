using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Application.Services;
using VinylStore.DataAccess.EF.Models;
using VinylStore.DataObjects.BusinessModels;
using VinylStore.Web.Validation.Attributes;
using VinylStore.Web.ViewModels;

namespace VinylStore.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AlbumsController : ControllerBase
{
    private readonly AlbumService _albumService;
    private readonly IMapper _mapper;

    public AlbumsController(
        AlbumService albumService,
        IMapper mapper
    )
    {
        _albumService = albumService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [Route("Get/{id}")]
    public AlbumViewModel GetById(Guid id)
    {
        var album = _albumService.GetById(id);

        var model = _mapper.Map<AlbumViewModel>(album);
        
        return model;
    }
    
    [HttpGet]
    [Route("GetAll")]
    public IEnumerable<AlbumViewModel> GetAll()
    {
        var albums = _albumService.GetAll();

        var models = albums.Select(album => _mapper.Map<AlbumViewModel>(album));
        
        return models;
    }
    
    [HttpPost]
    [Route("Create")]
    [Authorization(Role.Admin, Role.Client)]
    public IActionResult Create(AlbumViewModel model)
    {
        var album = _mapper.Map<CompleteAlbum>(model);
        
        var id = _albumService.CreateAlbum(album);

        return Ok(id);
    }
    
    [HttpPut]
    [Route("Update")]
    [Authorization(Role.Admin, Role.Client)]
    public IActionResult Update(AlbumViewModel model)
    {
        var album = _mapper.Map<CompleteAlbum>(model);

        _albumService.UpdateAlbum(album);
        
        return Ok();
    }
    
    [HttpDelete]
    [Route("Delete")]
    [Authorization(Role.Admin, Role.Client)]
    public IActionResult Delete([FromBody] Guid id)
    {
        _albumService.DeleteAlbum(id);

        return Ok();
    }
}
