using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace VinylStore.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMapper _mapper;
    
    public UsersController(IMapper mapper)
    {
        _mapper = mapper;
    }
}
