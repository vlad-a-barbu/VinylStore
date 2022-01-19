using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Application;
using VinylStore.Web.ViewModels;

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
    
    [HttpGet("{id}")]
    public GenreViewModel GetById(Guid id)
    {
        var genre = _genreService.GetById(id);

        var model = _mapper.Map<GenreViewModel>(genre);
        
        return model;
    }
    
    [HttpGet]
    public IEnumerable<GenreViewModel> GetAll()
    {
        var genres = _genreService.GetAll();

        var models = genres.Select(genre => _mapper.Map<GenreViewModel>(genre));
        
        return models;
    }
    
    [HttpPost]
    public IActionResult Create(GenreViewModel model)
    {
        var genre = _mapper.Map<DataObjects.Genre>(model);
        
        _genreService.CreateGenre(genre);

        return Ok();
    }
    
    [HttpPut]
    public IActionResult Update(GenreViewModel model)
    {
        var genre = _mapper.Map<DataObjects.Genre>(model);

        _genreService.UpdateGenre(genre);
        
        return Ok();
    }
    
    [HttpDelete]
    public IActionResult Delete(Guid id)
    {
        _genreService.DeleteGenre(id);

        return Ok();
    }
}
