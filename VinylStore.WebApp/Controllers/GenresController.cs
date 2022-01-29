using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Application.Services;
using VinylStore.DataAccess.EF.Models;
using VinylStore.Web.Validation.Attributes;
using VinylStore.Web.ViewModels;
using Genre = VinylStore.DataObjects.Entities.Genre;

namespace VinylStore.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class GenresController : ControllerBase
{
    private readonly GenreService _genreService;
    private readonly IMapper _mapper;

    public GenresController(
        GenreService genreService,
        IMapper mapper
    )
    {
        _genreService = genreService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [Route("Get/{id}")]
    public GenreViewModel GetById(Guid id)
    {
        var genre = _genreService.GetById(id);

        var model = _mapper.Map<GenreViewModel>(genre);
        
        return model;
    }
    
    [HttpGet]
    [Route("GetAll")]
    public IEnumerable<GenreViewModel> GetAll()
    {
        var genres = _genreService.GetAll();

        var models = genres.Select(genre => _mapper.Map<GenreViewModel>(genre));
        
        return models;
    }
    
    [HttpPost]
    [Route("Create")]
    [Authorization(Role.Admin, Role.Client)]
    public IActionResult Create(GenreViewModel model)
    {
        var genre = _mapper.Map<Genre>(model);
        
        var id = _genreService.CreateGenre(genre);

        return Ok(id);
    }
    
    [HttpPut]
    [Route("Update")]
    [Authorization(Role.Admin, Role.Client)]
    public IActionResult Update(GenreViewModel model)
    {
        var genre = _mapper.Map<Genre>(model);

        _genreService.UpdateGenre(genre);
        
        return Ok();
    }
    
    [HttpDelete]
    [Route("Delete")]
    [Authorization(Role.Admin)]
    public IActionResult Delete([FromBody] Guid id)
    {
        _genreService.DeleteGenre(id);

        return Ok();
    }
}
