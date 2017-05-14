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

namespace ActiveLearning.Repository.Repository
{
    public class Student_Course_MapRepository : Repository<Student_Course_Map>, IStudent_Course_MapRepository
    {
        public Student_Course_MapRepository(DbContext context)
            : base(context)
        {
        }
    }
}
