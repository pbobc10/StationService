using System.ComponentModel.DataAnnotations;

namespace StationService.DTOs
{
    public class AdministratorInputDto
    {
        public int Id { get; set; }

        [Required, StringLength(30, ErrorMessage = "FirstName cannot be longer than 30 characters.")]
        public string FirstName { get; set; }

        [Required, StringLength(50, ErrorMessage = "FamilyName cannot be longer than 50 characters.")]
        public string FamilyName { get; set; }

        [EmailAddress]
        public string Email { get; set; }


    }


}
