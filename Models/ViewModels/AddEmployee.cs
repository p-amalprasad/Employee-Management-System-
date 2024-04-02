using System.ComponentModel.DataAnnotations;

namespace LogGenerator.Models.ViewModels
{
    public class AddEmployee
    {
        public int EmpId { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string About { get; set; }
        public string JobDesc { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DoB { get; set; }
    }
}
