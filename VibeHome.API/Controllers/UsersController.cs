using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.API.Controllers
{
    public class UsersController : ODataController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [EnableQuery]
        public async Task<IActionResult> Get(int key)
        {
            var user = await _userService.GetByIdAsync(key);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        public async Task<IActionResult> Post([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userService.AddAsync(user);
            return Created(user);
        }

        public async Task<IActionResult> Put(int key, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != user.UserId)
            {
                return BadRequest();
            }

            var existing = await _userService.GetByIdAsync(key);
            if (existing == null)
            {
                return NotFound();
            }

            await _userService.UpdateAsync(user);
            return Updated(user);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var user = await _userService.GetByIdAsync(key);
            if (user == null)
            {
                return NotFound();
            }

            await _userService.DeleteAsync(key);
            return NoContent();
        }
    }
}
