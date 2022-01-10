using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string? Id { get; set; }

        public string? Role { get; set; }

        [Required]
        [MaxLength(32)]
        public string? Username { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(256)]
        public string? Password { get; set; }
    }
}