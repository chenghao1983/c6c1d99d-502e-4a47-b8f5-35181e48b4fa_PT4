using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace ActiveLearning.DB
{
    [MetadataType(typeof(QuizOptionMetadata))]
    public partial class QuizOption
    {
        public int CourseSid { get; set; }

        private class QuizOptionMetadata
        {
            [Required(ErrorMessage = Common.Constants.Please_Enter + "Quiz Option Title")]
            [Display(Name = "Quiz Option Title")]
            public string Title { get; set; }

            [JsonIgnore]
            [ScriptIgnore]
            public bool IsCorrect { get; set; }

            [JsonIgnore]
            [ScriptIgnore]
            [Display(Name = "Createdt Date")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
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
            public virtual ICollection<QuizAnswer> QuizAnswers { get; set; }

            [JsonIgnore]
            [ScriptIgnore]
            public virtual QuizQuestion QuizQuestion { get; set; }
        }
    }
}
