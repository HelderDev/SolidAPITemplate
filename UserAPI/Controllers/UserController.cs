using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Models.Entities;
using Service.Validators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IBaseService<UserModel> _baseUserService;

        public UserController(IBaseService<UserModel> baseUserService)
        {
            _baseUserService = baseUserService;
        }

        private async Task<IActionResult> ExecuteAsync(Func<Task<object>> func)
        {
            try
            {
                var result = await func();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserModel user)
        {
            if (user == null)
                return NotFound();
            return await ExecuteAsync(async () => (await _baseUserService.AddAsync<UserValidator>(user)).Id);
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserModel user)
        {
            if (user == null)
                return NotFound();
            return await ExecuteAsync(async () => await _baseUserService.UpdateAsync<UserValidator>(user));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return NotFound();
            await ExecuteAsync(async () =>
            {
                await _baseUserService.DeleteAsync(id);
                return true;
            });
            return new NoContentResult();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        => await ExecuteAsync(async () => await _baseUserService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
                return NotFound();
            return await ExecuteAsync(async() => await _baseUserService.GetByIdAsync(id));
        }


    }
}

