using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMangementSystem.Model.Models.Student;
using StudentMangementSystem.Model.Requst.Staff;
using StudentMangementSystem.Model.Requst.Student;
using StudentMangementSystem.Model.Response;
using StudentMicroService.Services.Interface;

namespace StudentMicroService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;
        private readonly IValidator<StudentRequest> _validator;

        public StudentsController(IStudentService service, IValidator<StudentRequest> validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(ResponseFactory.Success(data));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentRequest student)
        {
            if (student == null)
                return BadRequest("Invalid data");

            //Fluent Validation
            var validation = await _validator.ValidateAsync(student);
            if (!validation.IsValid)
            {
                return BadRequest(ResponseFactory.ModelValidation(validation.Errors));
            }

            var studentInfo = await _service.CreateAsync(student);
            return Ok(ResponseFactory.Success(studentInfo));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] StudentRequest student)
        {
            if (student == null || id != student.Id)
                return BadRequest("Invalid data");

            var exists = await _service.GetByIdAsync(id);
            if (exists == null)
                return NotFound("Student not found");

            await _service.UpdateAsync(student);

            return Ok(ResponseFactory.Success(student, "Updated Successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var exists = await _service.GetByIdAsync(id);
            if (exists == null)
                return NotFound("Student not found");

            await _service.DeleteAsync(id);

            return Ok(new { Message = "Student deleted successfully" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var student = await _service.GetByIdAsync(id);

            if (student == null)
                return NotFound("Student not found");

            return Ok(student);
        }
    }
}
