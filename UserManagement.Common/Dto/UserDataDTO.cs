using System.ComponentModel.DataAnnotations;

namespace UserManagement.Common.Dto
{
    public class UserDataDTO
    {
        public int? UserDataId { get; set; }
        
        [Required]
        public string Name { get; set; }

        public int? Age { get; set; }
        
        [Required]
        public string Gender { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$",
         ErrorMessage = "please enter valid email address eg: abc@domain.com")]
        public string Email { get; set; }

        public string Address { get; set; }

        [RegularExpression(@"^(91){0,1}[9876]{1}[0-9]{9}$",
 ErrorMessage = "please enter valid mobile number eg format: 919876543210 or 9876543210")]
        public string MobileNumber { get; set; }

        public string ProfilePictureBase64Data { get; set; }

        public string FileName { get; set; }
    }
}
