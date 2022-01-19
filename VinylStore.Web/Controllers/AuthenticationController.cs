using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VinylStore.Application;
using VinylStore.Web.Validation;
using VinylStore.Web.ViewModels;

namespace VinylStore.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly UserAuthenticationService _authenticationService;
    private readonly IMapper _mapper;

    public AuthenticationController(
        UserAuthenticationService authenticationService,
        IMapper mapper
    )
    {
        _authenticationService = authenticationService;
        _mapper = mapper;
    }

    [HttpPost("Register")]
    [ValidateModel]
    public IActionResult Register(RegisterViewModel model)
    {
        
        
        return Ok();
    }
}