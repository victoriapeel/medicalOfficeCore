using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace medicalOfficeCore.Models
{
    public class Patient
    {
        public int ID { get; set; } //system will by default make anything called ID or className.ID the primary key

        [Display(Name = "Patient")]
        public string FullName
        {
            get
            {
                return FirstName
                    + (string.IsNullOrEmpty(MiddleName) ? " " :
                        (" " + (char?)MiddleName[0] + ". ").ToUpper())
                    + LastName;
            }
        }

        public string Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int? a = today.Year - DOB?.Year
                    - ((today.Month < DOB?.Month || (today.Month == DOB?.Month && today.Day < DOB?.Day) ? 1 : 0));
                return a?.ToString(); /*Note: You could add .PadLeft(3) but spaces disappear in a web page. */
            }
        }

        [Required(ErrorMessage = "You cannot leave the OHIP number blank.")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "The OHIP number must be exactly 10 numeric digits.")]
        [StringLength(10)]//DS Note: we only include this to limit the size of the database field to 10
        public string OHIP { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You cannot leave the first name blank.")]
        [StringLength(50, ErrorMessage = "First name cannot be more than 50 characters long.")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(50, ErrorMessage = "Middle name cannot be more than 50 characters long.")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You cannot leave the last name blank.")]
        [StringLength(100, ErrorMessage = "Last name cannot be more than 100 characters long.")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DOB { get; set; }

        [Display(Name = "Visits/Yr")]
        [Required(ErrorMessage = "You cannot leave the number of expected vists per year blank.")]
        [Range(1, 12, ErrorMessage = "The number of expected vists per year must be between 1 and 12.")]
        public byte ExpYrVisits { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}", ApplyFormatInEditMode = false)]
        public Int64 Phone { get; set; }

        [Required(ErrorMessage = "Email Address is required.")]
        [StringLength(255)]
        [DataType(DataType.EmailAddress)]
        public string eMail { get; set; }

        [Required(ErrorMessage = "You must select a Primary Care Physician.")]
        [Display(Name = "Doctor")]
        public int DoctorID { get; set; } //foreign key

        public virtual Doctor Doctor { get; set; } //defines the referential integrity -- foreign key
    }
}
