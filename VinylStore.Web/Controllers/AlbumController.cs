using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Application.Services;
using VinylStore.DataAccess.EF.Models;
using VinylStore.DataObjects.BusinessModels;
using VinylStore.Web.Validation.Attributes;
using VinylStore.Web.ViewModels;
using Album = VinylStore.DataObjects.Entities.Album;

namespace VinylStore.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AlbumController : ControllerBase
{
    private readonly AlbumService _albumService;
    private readonly IMapper _mapper;

    public AlbumController(
        AlbumService albumService,
        IMapper mapper
    )
    {
        _albumService = albumService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [Route("Get/{id}")]
    [Authorization(Role.Admin, Role.Client)]
    public AlbumViewModel GetById(Guid id)
    {
        var album = _albumService.GetById(id);

        var model = _mapper.Map<AlbumViewModel>(album);
        
        return model;
    }
    
    [HttpGet]
    [Route("GetAll")]
    [Authorization(Role.Admin, Role.Client)]
    public IEnumerable<AlbumViewModel> GetAll()
    {
        var albums = _albumService.GetAll();

        var models = albums.Select(album => _mapper.Map<AlbumViewModel>(album));
        
        return models;
    }
    
    [HttpPost]
    [Route("Create")]
    [Authorization(Role.Admin)]
    public IActionResult Create(AlbumViewModel model)
    {
        var album = _mapper.Map<CompleteAlbum>(model);
        
        _albumService.CreateAlbum(album);

        return Ok();
    }
    
    [HttpPut]
    [Route("Update")]
    [Authorization(Role.Admin)]
    public IActionResult Update(AlbumViewModel model)
    {
        var album = _mapper.Map<CompleteAlbum>(model);

        _albumService.UpdateAlbum(album);
        
        return Ok();
    }
    
    [HttpDelete]
    [Route("Delete")]
    [Authorization(Role.Admin)]
    public IActionResult Delete(Guid id)
    {
        _albumService.DeleteAlbum(id);

        return Ok();
    }
}
