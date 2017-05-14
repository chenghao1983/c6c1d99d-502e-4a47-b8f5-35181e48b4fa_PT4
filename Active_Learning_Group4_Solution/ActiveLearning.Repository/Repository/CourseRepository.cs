using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveLearning.Repository.Interface;
using ActiveLearning.Repository.Context;
using ActiveLearning.Repository.Repository.Core;
using ActiveLearning.DB;
using System.Linq.Expressions;

namespace ActiveLearning.Repository.Repository
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(DbContext context) : base(context)
        {
        }


    }
}
