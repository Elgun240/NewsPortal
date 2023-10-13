using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practice_3.ViewModels
{
    public class RegisterVM
    {
        [Required]
        public string UserName { get; set; }
        [Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        [Required,DataType(DataType.Password),Compare(nameof(Password))]
        public string CheckPassword { get; set; }
        public string Image { get;set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
