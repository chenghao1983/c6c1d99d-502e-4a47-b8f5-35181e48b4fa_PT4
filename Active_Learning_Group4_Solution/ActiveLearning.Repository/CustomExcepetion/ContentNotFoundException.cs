using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveLearning.Repository.CustomExcepetion
{
    public class ContentNotFoundException : Exception
    {
        public ContentNotFoundException()
        {

        }

        public ContentNotFoundException(string message) : base(message)
        {

        }
    }
}
