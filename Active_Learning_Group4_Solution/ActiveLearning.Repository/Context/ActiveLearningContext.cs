using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ActiveLearning.DB;

namespace ActiveLearning.Repository.Context
{
    public class ActiveLearningContext : DbContext
    {
        public ActiveLearningContext()
            : base("name=Active_Learning_Group4")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Admin> Authors { get; set; }
        public virtual DbSet<Chat> ChatDetails { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Instructor_Course_Map> Instructor_Course_Maps { get; set; }
        public virtual DbSet<QuizAnswer> QuizAnswers { get; set; }
        public virtual DbSet<QuizQuestion> QuizQuestions { get; set; }
        public virtual DbSet<QuizOption> QuizOptions { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Student_Course_Map> Student_Course_Maps { get; set; }
        public virtual DbSet<User> Users { get; set; }

    }
}
