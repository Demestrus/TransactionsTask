using System.ComponentModel.DataAnnotations;

namespace TransactionTask.WebApi.Models
{
    public class UserDto
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Слишком длинное имя (макс. 30 символов)")]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(30, ErrorMessage = "Слишком длинная фамилия (макс. 30 символов)")]
        public string Surname { get; set; }
    }
}