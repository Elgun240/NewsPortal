using Practice_3.Models;
using System.ComponentModel.DataAnnotations;

namespace Practice_3.ViewModels
{
    public class ChangePasswordVM
    {
        public string Password { get; set; }
        [Required, DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required, DataType(DataType.Password), Compare(nameof(NewPassword)) ]
        public string CheckPassword { get; set; }
        public AppUser User { get; set; }
    }
    
}
