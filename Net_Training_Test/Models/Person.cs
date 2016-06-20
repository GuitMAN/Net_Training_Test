using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Net_Training_Test.Models
{
    //The class contains the storage model information for each person. 
    public class Person
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Required field")]
        [DisplayName("Surname:")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Required field")]
        [DisplayName("Name:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required field")]
        [DisplayName("Year of Born:")]
        [Range(1900, 2016, ErrorMessage = "Invalid year")]
        public int YearBorn { get; set; }

        [Required(ErrorMessage = "Required field")]
        [DisplayName("Phone:")]
        public string Phone { get; set; }
    }
}