using System.ComponentModel.DataAnnotations;

namespace StationService.DTOs
{
    public class AdministratorInputDto
    {
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string FamilyName { get; set; }

        [EmailAddress]
        public string Email { get; set; }


    }


}
