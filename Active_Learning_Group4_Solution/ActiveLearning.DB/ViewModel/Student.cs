using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveLearning.DB;
using System.ComponentModel.DataAnnotations;

namespace ActiveLearning.DB
{
    [MetadataType(typeof(StudentMetadata))]
    public partial class Student
    {
        private class StudentMetadata
        {
            [Required(ErrorMessage = Common.Constants.Please_Enter + "Batch Number")]
            [Display(Name = "Batch Number")]
            public string BatchNo { get; set; }
        }

        [Display(Name = "Enrolled")]
        public bool HasEnrolled { get; set; }

    }
}
