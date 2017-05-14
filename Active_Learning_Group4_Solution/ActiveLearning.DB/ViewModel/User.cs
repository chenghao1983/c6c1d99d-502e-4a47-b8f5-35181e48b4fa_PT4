using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveLearning.DB;
using System.ComponentModel.DataAnnotations;

namespace ActiveLearning.DB
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
        public class UserMetadata
        {
            [Required(ErrorMessage = Common.Constants.Please_Enter + "User Name")]
            [Display(Name = "User Name")]
            public string Username { get; set; }

            [Required(ErrorMessage = Common.Constants.Please_Enter + "Full Name")]
            [Display(Name = "Full Name")]
            public string FullName { get; set; }

            [Display(Name = "Is Active")]
            public bool IsActive { get; set; }

            [Required(ErrorMessage = Common.Constants.Please_Enter + "Password")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Display(Name = "Created Date")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
            public DateTime CreateDT { get; set; }

            [Display(Name = "Updated Date")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",NullDisplayText ="-")]
            public DateTime? UpdateDT { get; set; }

            [Display(Name = "Deleted Date")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", NullDisplayText = "-")]
            public DateTime? DeleteDT { get; set; }


        }
    }
}
