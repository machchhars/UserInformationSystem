using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UserManagement.Common.DbModel
{
    public class UserData
    {
        public int UserDataId { get; set; }
        
        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Name { get; set; }

        public int? Age { get; set; }
        
        [Required]
        [Column(TypeName = "varchar(7)")]
        public string Gender { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string Address { get; set; }
        
        public long? MobileNumber { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string ProfilePictureBase64Data { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string FileName { get; set; }
    }
}
