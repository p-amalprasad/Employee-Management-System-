using System.ComponentModel.DataAnnotations;

namespace LogGenerator.Models.ViewModels
{
    public class AddCustomer
    {
        public int CustId { get; set; }
        public string Company { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string CustType { get; set; }
        public string Address { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CustSince { get; set; }
    }
}
