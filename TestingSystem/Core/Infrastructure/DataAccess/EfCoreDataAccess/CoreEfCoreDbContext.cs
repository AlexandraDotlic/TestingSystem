using Common.EfCoreDataAccess;
using Core.Domain.Entites;
using Core.Infrastructure.DataAccess.EfCoreDataAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.DataAccess.EfCoreDataAccess
{
    public class CoreEfCoreDbContext : EfCoreDbContext
    {

        public CoreEfCoreDbContext(DbContextOptions<CoreEfCoreDbContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Examiner> Examiners { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<StudentTest> StudentTests { get; set; }
        public DbSet<StudentTestQuestion> StudentTestQuestions { get; set; }
        public DbSet<StudentTestQuestionResponse> StudentTestQuestionResponses { get; set; }
        public DbSet<TestStatistic> TestStatistics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExaminerConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new TestConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionConfiguration());
            modelBuilder.ApplyConfiguration(new AnswerOptionConfiguration());
            modelBuilder.ApplyConfiguration(new StudentTestConfiguration());
            modelBuilder.ApplyConfiguration(new StudentTestQuestionConfiguration());
            modelBuilder.ApplyConfiguration(new StudentTestQuestionResponseConfiguration());


            base.OnModelCreating(modelBuilder);
        }
    }
}
