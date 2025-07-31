using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Interfaces;
using ShoppingApp.Models;
using ShoppingApp.Models.DTOs;

namespace ShoppingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IContactUService _contactUService;

        public ContactUsController(IContactUService contactUService)
        {
            _contactUService = contactUService;
        }

        [HttpPost("add")]
        [Authorize] 
        public async Task<ActionResult<ContactU>> AddContactU([FromBody] ContactURequestDto dto)
        {
            try
            {
                var contact = await _contactUService.AddContactU(dto);
                return Ok(contact);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")] 
        public async Task<ActionResult> DeleteContactU(int id)
        {
            try
            {
                var result = await _contactUService.DeleteContactUsById(id);
                return result ? Ok("Deleted successfully.") : NotFound();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
