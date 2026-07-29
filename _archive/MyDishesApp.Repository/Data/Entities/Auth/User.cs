using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDishesApp.Repository.Data.Entities.Auth
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }
        public string PasswordSalt { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
