using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveLearning.DB;
using System.ComponentModel.DataAnnotations;

namespace ActiveLearning.DB
{
    [MetadataType(typeof(InstructorMetadata))]
    public partial class Instructor
    {
        [Display(Name = "Enrolled")]
        public bool HasEnrolled { get; set; }

        public class InstructorMetadata
        {
            [Required(ErrorMessage = Common.Constants.Please_Enter + "Qualification")]
            [Display(Name = "Qualification")]
            public string Qualification { get; set; }
        }
    }
}
