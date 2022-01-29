using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Application.Services;
using VinylStore.DataObjects.AuthenticationModels;
using VinylStore.Web.Authorization;
using VinylStore.Web.Validation.Attributes;
using VinylStore.Web.ViewModels;

namespace VinylStore.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly UserAuthenticationService _authenticationService;
    private readonly IMapper _mapper;
    private readonly JwtUtils _utils;
    
    public AuthenticationController(
        UserAuthenticationService authenticationService,
        IMapper mapper,
        JwtUtils utils
    )
    {
        _authenticationService = authenticationService;
        _mapper = mapper;
        _utils = utils;
    }

    [HttpPost("RegisterClient")]
    [ValidateModel]
    public IActionResult RegisterClient(RegisterClientViewModel model)
    {
        var registerUser = _mapper.Map<RegisterUser>(model);
        
        var id = _authenticationService.RegisterClient(registerUser);
        
        return Ok(id);
    }
    
    [HttpPost("RegisterAdmin")]
    [ValidateModel]
    public IActionResult RegisterAdmin(RegisterAdminViewModel model)
    {
        var registerUser = _mapper.Map<RegisterUser>(model);
        
        var id = _authenticationService.RegisterAdmin(registerUser);
        
        return Ok(id);
    }
    
    [HttpPost("Authenticate")]
    [ValidateModel]
    public IActionResult Authenticate(LoginViewModel model)
    {
        var loginUser = _mapper.Map<LoginUser>(model);

        var authenticatedUser = _authenticationService.Authenticate(loginUser);
        
        return authenticatedUser is not null ? 
            Ok(_utils.GenerateToken(authenticatedUser)) : BadRequest();
    }
}
