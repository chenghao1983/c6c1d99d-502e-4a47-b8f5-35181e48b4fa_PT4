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
    public class QuizOptionRepository : Repository<QuizOption>, IQuizOptionRepository
    {
        public QuizOptionRepository(DbContext context)
            : base(context)
        {
        }

    }
}
