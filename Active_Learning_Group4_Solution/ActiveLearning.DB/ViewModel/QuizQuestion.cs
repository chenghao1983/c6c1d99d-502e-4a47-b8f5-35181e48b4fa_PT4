using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace ActiveLearning.DB
{
    [MetadataType(typeof(QuizQuestionMetadata))]
    public partial class QuizQuestion
    {
        private class QuizQuestionMetadata
        {
            [Required(ErrorMessage = Common.Constants.Please_Enter + "Quiz Question Title")]
            [Display(Name = "Quiz Question Title")]
            public string Title { get; set; }

            [JsonIgnore]
            [ScriptIgnore]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
            [Display(Name = "Createdt Date")]
            public System.DateTime CreateDT { get; set; }

            [JsonIgnore]
            [ScriptIgnore]
            [Display(Name = "Updated Date")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", NullDisplayText = "-")]
            public Nullable<System.DateTime> UpdateDT { get; set; }

            [JsonIgnore]
            [ScriptIgnore]
            [Display(Name = "Deleted Date")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", NullDisplayText = "-")]
            public Nullable<System.DateTime> DeleteDT { get; set; }

            [JsonIgnore]
            [ScriptIgnore]
            public int CourseSid { get; set; }

            [JsonIgnore]
            [ScriptIgnore]
            public virtual Course Course { get; set; }
        }
    }
}
