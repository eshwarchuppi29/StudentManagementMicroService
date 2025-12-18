using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaffMicroService.Services.Interfaces;
using StudentMangementSystem.Model.Requst.Staff;
using StudentMangementSystem.Model.Response;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace StaffMicroService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _service;
        private readonly IValidator<StaffRequest> _validator;

        public StaffController(IStaffService service, IValidator<StaffRequest> validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _service.GetAllStaffAsync();
            return Ok(ResponseFactory.Success(data));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StaffRequest staffRequest)
        {
            var validation = await _validator.ValidateAsync(staffRequest);
            if (!validation.IsValid)
            {
                return BadRequest(ResponseFactory.ModelValidation(validation.Errors));
            }
            var response = await _service.CreateStaffAsync(staffRequest);
            return Ok(ResponseFactory.Success(response));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid Id, [FromBody] StaffRequest staffRequest)
        {
            var validation = await _validator.ValidateAsync(staffRequest);
            if (!validation.IsValid)
            {
                return BadRequest(ResponseFactory.ModelValidation(validation.Errors));
            }

            var response = await _service.UpdateStaffAsync(Id, staffRequest);
            return Ok(ResponseFactory.Success(response));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
