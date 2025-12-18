using FluentValidation;
using StudentMangementSystem.Model.Requst.Student;
using StudentMangementSystem.Model.Validation;

namespace StudentMicroService.Validations
{
    public class StudentRequestValidation : AbstractValidator<StudentRequest>
    {
        public StudentRequestValidation()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Name is required").WithErrorCode(ValidationErrorCodes.NameRequired)
                          .Matches("^[A-Za-z ]+$").WithMessage("Name should not containe any special charcaters and numbers").WithErrorCode(ValidationErrorCodes.InvalidName)
                          .Length(3, 50).WithMessage("Name must be with 3 to 50 Characters").WithErrorCode(ValidationErrorCodes.InvalidLength);
        }
    }
}
