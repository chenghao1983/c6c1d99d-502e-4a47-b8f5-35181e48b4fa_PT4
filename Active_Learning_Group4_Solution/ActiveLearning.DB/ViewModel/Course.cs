using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveLearning.DB;
using System.ComponentModel.DataAnnotations;
using ActiveLearning.DB.Common;

namespace ActiveLearning.DB
{
    [MetadataType(typeof(CourseMetadata))]
    public partial class Course
    {
        public class CourseMetadata
        {
            [Required(ErrorMessage = Constants.Please_Enter + "Course Name")]
            [Display(Name = "Course Name")]
            public string CourseName { get; set; }

            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
            [Display(Name = "Created Date")]
            public DateTime CreateDT { get; set; }

            [Display(Name = "Updated Date")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", NullDisplayText = "-")]
            public DateTime? UpdateDT { get; set; }

            [Display(Name = "Deleted Date")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", NullDisplayText = "-")]
            public DateTime? DeleteDT { get; set; }
        }

    }
}
