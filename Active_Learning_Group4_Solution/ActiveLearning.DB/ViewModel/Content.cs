using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveLearning.DB;
using System.ComponentModel.DataAnnotations;

namespace ActiveLearning.DB
{
    [MetadataType(typeof(ContentMetadata))]
    public partial class Content
    {
        public class ContentMetadata
        {
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
            [Display(Name = "Created Date")]
            public DateTime CreateDT { get; set; }

            [Display(Name = "Updated Date")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", NullDisplayText = "-")]
            public DateTime? UpdateDT { get; set; }

            [Display(Name = "Deleted Date")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", NullDisplayText = "-")]
            public DateTime? DeleteDT { get; set; }

            [Display(Name = "Content Type")]
            [DisplayFormat(NullDisplayText = "-")]
            public string Type { get; set; }

            [Display(Name = "Content Path")]
            [DisplayFormat(NullDisplayText = "-")]
            public string Path { get; set; }

            [Display(Name = "File Name")]
            [DisplayFormat(NullDisplayText = "-")]
            public string FileName { get; set; }

            [Display(Name = "Original File Name")]
            [DisplayFormat(NullDisplayText = "-")]
            public string OriginalFileName { get; set; }

        }
    }
}
