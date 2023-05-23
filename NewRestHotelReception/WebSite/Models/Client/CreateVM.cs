using ApplicationService.DTOs;
using System.ComponentModel.DataAnnotations;

namespace WebSite.Models.Client
{
    public class CreateVM
    {
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Phone number")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public CreateVM() { }
        public CreateVM(ClientDTO clientDTO)
        {
            FirstName = clientDTO.FirstName;
            LastName = clientDTO.LastName;
            Phone = clientDTO.Phone;
            City = clientDTO.City;
            Email = clientDTO.Email;


        }
    }
}
