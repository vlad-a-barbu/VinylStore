using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Application;
using VinylStore.DataObjects;
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
    public Genre GetById(Guid id)
    {
        return _genreService.GetById(id);
    }
    
    [HttpGet]
    public IEnumerable<Genre> GetAll()
    {
        return _genreService.GetAll();
    }
    
    [HttpPost]
    public void Create(CreateGenre genre)
    {
        _genreService.CreateGenre(_mapper.Map<Genre>(genre));
    }
    
    [HttpPut]
    public void Update(Genre genre)
    {
        _genreService.UpdateGenre(genre);
    }
    
    [HttpDelete]
    public void Delete(Guid id)
    {
        _genreService.DeleteGenre(id);
    }
}
