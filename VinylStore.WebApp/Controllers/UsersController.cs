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
public class UsersController : ControllerBase
{
    private readonly UserService _userService;
    private readonly IMapper _mapper;
    
    public UsersController(
        UserService userService,
        IMapper mapper
    )
    {
        _userService = userService;
        _mapper = mapper;
    }
        
    [HttpGet]
    [Route("Get/{id}")]
    public UserViewModel GetById(Guid id)
    {
        var user = _userService.GetById(id);

        var model = _mapper.Map<UserViewModel>(user);
        
        return model;
    }
    
    [HttpGet]
    [Route("GetAll")]
    public IEnumerable<UserViewModel> GetAll()
    {
        var users = _userService.GetAll();

        var models = users.Select(user => _mapper.Map<UserViewModel>(user));
        
        return models;
    }
    
    [HttpPut]
    [Route("Update")]
    public IActionResult Update(UserViewModel model)
    {
        var user = _mapper.Map<CompleteUser>(model);

        _userService.UpdateUser(user);
        
        return Ok();
    }
    
    [HttpDelete]
    [Route("Delete")]
    public IActionResult Delete(Guid id)
    {
        _userService.DeleteUser(id);

        return Ok();
    }
}
