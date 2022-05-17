using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Common.Dto;
using UserManagement.Common.Interface;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<UserDataDTO>> Get()
        {
            return await _userRepository.GetAllUser();
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] UserDataDTO userDataDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _userRepository.AddAsync(userDataDTO);

                return Ok(new { Message = "User information added successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContactDetail([FromRoute] int id)
        {
            try
            {
                _userRepository.DeleteUser(id);

                return Ok(new { Message = "User deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserDataDTO userDataDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _userRepository.UpdateAsync(userDataDTO);

                return Ok(new { Message = "User information updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
