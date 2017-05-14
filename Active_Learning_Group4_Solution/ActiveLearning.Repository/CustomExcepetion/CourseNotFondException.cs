using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveLearning.Repository.CustomExcepetion
{
    public class CourseNotFondException : System.Exception
    {
        public CourseNotFondException()
        {
        }

        public CourseNotFondException(string message) : base(message)
        {
        }

    }
}
