using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

        [NotMapped]
        [JsonIgnore]
        private DateOnly dob =
            new DateOnly(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day
            );

        [NotMapped]
        [JsonIgnore]
        public DateOnly DateOfBirthFormat
        {
            get {
                var dateTime = Convert.ToDateTime(DateOfBirth);
                var dateOnly = new DateOnly(
                    dateTime.Year,
                    dateTime.Month,
                    dateTime.Day
                );
                dob = dateOnly;
                return dob;
            }
            set { dob = value; }
        }

        public string DateOfBirth
        {
            get { return dob.ToString(); }
        }

        public string? ContactNumber { get; set; }

        // [Required]
        public string? PictureProfile { get; set; }

    }
}