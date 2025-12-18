using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMangementSystem.Model.Validation
{
    public class ValidationErrorCodes
    {
        public const string NameRequired = "WARNING_NAME_REQUIRED";
        public const string DateRequired = "WARNING_DATE_REQUIRED";
        public const string MobileNumberRequired = "WARNING_MOBILE_REQUIRED";
        public const string AdharNumberRequired = "WARNING_ADHAR_REQUIRED";
        public const string PANNumberRequired = "WARNING_ADHAR_REQUIRED";
        public const string EmailRequired = "WARNING_EMAIL_REQUIRED";
        public const string QualificationRequired = "WARNING_QUALIFICATION_REQUIRED";
        public const string DesignationRequired = "WARNING_DESIGNATION_REQUIRED";

        public const string InvalidName = "ERROR_NAME_INVALID";
        public const string InvalidLength = "ERROR_INVALID_LENGTH";
        public const string InvalidEmail = "ERROR_INVALID_EMAIL";
        public const string InvalidMobile = "ERROR_INVALID_MOBILE";
        public const string InvalidSalary = "ERROR_INVALID_SALARY";
        public const string InvalidAdharNumber = "ERROR_INVALID_ADHAR";
        public const string InvalidPANNumber = "ERROR_INVALID_PAN";
        public const string InvalidQualification = "ERROR_INVALID_QUALIFICATION";
        public const string InvalidDesignation = "ERROR_INVALID_DESIGNATION";
    }
}
