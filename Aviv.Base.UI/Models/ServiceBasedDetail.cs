using System.ComponentModel.DataAnnotations;

namespace Aviv.Base.UI.Models
{
    public class ServiceBasedDetail
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Service name is required")]
        [Display(Name = "Service Name")]
        public string? Service_Name { get; set; }

        [Required(ErrorMessage = "Type of services provided is required")]
        [Display(Name = "Service Type")]
        public string? Type_of_Services_Provided { get; set; }

        [Required(ErrorMessage = "Average service turnaround time is required")]
        [Display(Name = "Turnaround Time")]
        public string? Average_Service_Turnaround_Time { get; set; }

        // File upload related properties
        public string? Certification_Filename { get; set; }
        public bool Has_Certifications { get; set; }
    }
}