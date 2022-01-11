using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "User ID")]
        public string? Id { get; init; }

        public string? Role { get; set; }

        [Required]
        [MaxLength(32)]
        public string? Username { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(256)]
        public string? Password { get; set; }

        [NotMapped]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }

        [EmailAddress]
        public string? EmailAddress { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        [NotMapped]
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }

        public DateOnly DateOfBirth { get; set; } =
            new DateOnly(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day
            );

        public string? ContactNumber { get; set; }

        public string? PictureProfile { get; set; }
    }
}