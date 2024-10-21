using Application.Exceptions;
using Application.Interface.InterfaceService;
using Application.Models.RequestDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task <IActionResult> getByID(int id)
        {
            try
            {
                var user = await _service.GetById(id);
                return Ok(user);
            }
            catch(NotFoundException ex)
            {
                return NotFound(new {message = ex.Message});
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser(UserRequestDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "The data provided is not valid.", errors = ModelState });
                }
                var user = await _service.CreateUser(dto);
                return Created(string.Empty, user);
            }
            catch(DuplicateEntityException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch(ArgumentOutOfRangeException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser(UpdateUserRequestDTO userRequestDTO, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "The data provided is not valid.", errors = ModelState });
                }
                var user = await _service.UpdateUser(userRequestDTO,id);
                return Ok(user);
            }
            catch(NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (DuplicateEntityException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
