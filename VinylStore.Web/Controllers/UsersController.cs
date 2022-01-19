using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Application;
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
        
    [HttpGet("{id}")]
    public UserViewModel GetById(Guid id)
    {
        var user = _userService.GetById(id);

        var model = _mapper.Map<UserViewModel>(user);
        
        return model;
    }
    
    [HttpGet]
    public IEnumerable<UserViewModel> GetAll()
    {
        var users = _userService.GetAll();

        var models = users.Select(user => _mapper.Map<UserViewModel>(user));
        
        return models;
    }
    
    [HttpPut]
    public IActionResult Update(UserViewModel model)
    {
        var user = _mapper.Map<DataObjects.User>(model);

        _userService.UpdateUser(user);
        
        return Ok();
    }
    
    [HttpDelete]
    public IActionResult Delete(Guid id)
    {
        _userService.DeleteUser(id);

        return Ok();
    }
}
