using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coqueta.Types
{
    public class User : TypeBase
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nchar")]
        [StringLength(8, ErrorMessage = "No mas de 8 caracteres")]
        public string Username { get; set; }

        [Required]
        [Column(TypeName = "nchar")]
        [StringLength(60, ErrorMessage = "No mas de 60 caracteres")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",
            ErrorMessage = "Formato de email no valido")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "nchar")]
        public string Password { get; set; }

        [Required]
        [Column(TypeName = "nchar")]
        [Compare("Password")]
        public string ConfirmationPassword { get; set; }
    }
}
