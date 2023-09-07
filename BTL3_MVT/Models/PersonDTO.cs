using System.ComponentModel.DataAnnotations;

namespace BTL3_MVT.Models
{
    public class PersonDTO
    {
        public int PersonId { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(50, ErrorMessage = "Độ dài tối đa là 50 kí tự")]
        [RegularExpression(@"^[^0-9]*$", ErrorMessage = "Tên Không được chứa kí tự số")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [ValidBirthDay(ErrorMessage = "Năm sinh không hợp lệ")]
        public DateTime BirthDay { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        public string Gender { get; set; }

        [NotZeroOrEmpty(ErrorMessage = "Không được để trống")]
        public int WorkId { get; set; }

        [NotZeroOrEmpty(ErrorMessage = "Không được để trống")]
        public int EthnicityId { get; set; }

        [RegularExpression(@"^\d{12}$", ErrorMessage = "Số CMND phải có 12 chữ số")]
        public string? IdentityCard { get; set; }

        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string? PhoneNumber { get; set; }

        [NotZeroOrEmpty(ErrorMessage = "Không được để trống")]
        public int CityId { get; set; }

        [NotZeroOrEmpty(ErrorMessage = "Không được để trống")]
        public int DistrictId { get; set; }

        [NotZeroOrEmpty(ErrorMessage = "Không được để trống")]
        public int WardId { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(20, ErrorMessage = "Độ dài tối đa là 20 kí tự")]
        public string SpecificAddress { get; set; }

        [NotZeroOrEmpty(ErrorMessage = "Không được để trống")]
        public int WorkCityId { get; set; }

        [NotZeroOrEmpty(ErrorMessage = "Không được để trống")]
        public int WorkDistrictId { get; set; }

        [NotZeroOrEmpty(ErrorMessage = "Không được để trống")]
        public int WorkWardId { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(20, ErrorMessage = "Độ dài tối đa là 20 kí tự")]
        public string WorkSpecificAddress { get; set; }
    }

    public class NotZeroOrEmptyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is int intValue)
            {
                return intValue != 0;
            }

            return value != null;
        }
    }
    public class ValidBirthDayAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime birthDay)
            {
                var currentYear = DateTime.Now.Year;
                var minValidYear = currentYear - 100;

                if (birthDay.Year >= minValidYear && birthDay.Year <= currentYear)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            else
            {
                return new ValidationResult(ErrorMessage);
            }
        }
    }
}
