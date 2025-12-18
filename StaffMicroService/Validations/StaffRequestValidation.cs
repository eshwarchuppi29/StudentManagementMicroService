using FluentValidation;
using StudentMangementSystem.Model.Requst.Staff;
using StudentMangementSystem.Model.Validation;

namespace StaffMicroService.Validations
{
    public class StaffRequestValidation : AbstractValidator<StaffRequest>
    {
        public StaffRequestValidation()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Name is required").WithErrorCode(ValidationErrorCodes.NameRequired)
                                     .Matches("^[A-Za-z ]+$").WithMessage("Name should not containe any special charcaters and numbers").WithErrorCode(ValidationErrorCodes.InvalidName)
                                     .Length(3, 50).WithMessage("Name must be with 3 to 50 Characters").WithErrorCode(ValidationErrorCodes.InvalidLength);

            RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Date is reqired").WithErrorCode(ValidationErrorCodes.DateRequired);

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.").EmailAddress().WithMessage(ValidationErrorCodes.InvalidEmail);

            RuleFor(x => x.MobileNumber).NotEmpty().WithMessage("Mobile Number is required.").WithErrorCode(ValidationErrorCodes.MobileNumberRequired)
                                        .Matches(@"^[6-9]\d{9}$").WithMessage("Invalid Indian mobile number. It must be 10 digits starting with 6,7,8 or 9.").WithErrorCode(ValidationErrorCodes.InvalidMobile);

            RuleFor(x => x.AdharNumber).NotEmpty().WithMessage("Aadhaar number is required.").WithErrorCode(ValidationErrorCodes.AdharNumberRequired)
                                       .Matches(@"^\d{12}$").WithMessage("Aadhaar number must be 12 digits.").WithErrorCode(ValidationErrorCodes.InvalidAdharNumber);

            RuleFor(x => x.PANNumber).NotEmpty().WithMessage("PAN number is required.").WithErrorCode(ValidationErrorCodes.PANNumberRequired)
                                     .Matches(@"^[A-Z]{5}[0-9]{4}[A-Z]{1}$").WithMessage("Invalid PAN format. Example: ABCDE1234F").WithErrorCode(ValidationErrorCodes.InvalidPANNumber);

            RuleFor(x => x.Qualification).NotEmpty().WithMessage("Qualification is required").WithErrorCode(ValidationErrorCodes.QualificationRequired)
                         .Length(3, 50).WithMessage("Name must be with 3 to 50 Characters").WithErrorCode(ValidationErrorCodes.InvalidLength);

            RuleFor(x => x.Designation).NotEmpty().WithMessage("Designation is required").WithErrorCode(ValidationErrorCodes.DesignationRequired)
                         .Matches("^[A-Za-z ]+$").WithMessage("Name should not containe any special charcaters and numbers").WithErrorCode(ValidationErrorCodes.InvalidDesignation)
                         .Length(3, 50).WithMessage("Name must be with 3 to 50 Characters").WithErrorCode(ValidationErrorCodes.InvalidLength);
        }
    }
}
