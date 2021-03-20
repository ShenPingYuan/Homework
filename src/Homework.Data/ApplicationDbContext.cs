using Homework.DB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Homework.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions option) : base(option)
        {
        }
        public static readonly ILoggerFactory Homework_EFLoggerFactory = LoggerFactory.Create(config =>
        {
            config.AddConsole();
        });
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //配置实体之间的关系
            builder.Entity<Teacher>()
                .HasOne(t => t.Course)
                .WithOne(c => c.Teacher)
                .HasForeignKey<Teacher>(t => t.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Choice>()
                .HasOne(c => c.Question)
                .WithMany(q => q.Choices)
                .HasForeignKey(c => c.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Question>()
                .HasOne(l => l.Homework)
                .WithMany(l => l.Questions)
                .HasForeignKey(l => l.HomeworkId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<StudentAnswer>()
                .HasOne(l => l.StudentWork)
                .WithMany(l => l.StudentAnswers)
                .HasForeignKey(l => l.StudentWorkId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<DB.Entities.Homework>()
                .HasOne(l => l.Course)
                .WithMany(l => l.Homeworks)
                .HasForeignKey(l => l.CourseId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<StudentWork>()
                .HasOne(l => l.Student)
                .WithMany(l => l.StudentWorks)
                .HasForeignKey(l => l.StudentId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<StudentWork>()
                .HasOne(l => l.Homework)
                .WithMany(l => l.StudentWorks)
                .HasForeignKey(l => l.HomeworkId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<StudentCourse>()
                .HasOne(l => l.Student)
                .WithMany(l => l.StudentCourses)
                .HasForeignKey(l => l.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<StudentCourse>()
                .HasOne(l => l.Course)
                .WithMany(l => l.StudentCourses)
                .HasForeignKey(l => l.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
            //配置实体属性
            builder.Entity<Teacher>(entity =>
            {
                entity.HasKey(l => l.TeacherId);
                entity.ToTable("tb_teacher");
                entity.Property(o => o.TeacherName)
                .HasMaxLength(20)
                .IsUnicode(true);
                entity.HasIndex(o => o.Sub).IsUnique(true);
            });
            builder.Entity<StudentCourse>()
                .HasKey(o => new { o.CourseId, o.StudentId });
            builder.Entity<Student>(entity => entity.HasIndex(o => o.Sub).IsUnique(true));
        }
        public virtual DbSet<Choice> Choices { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<DB.Entities.Homework> Homeworks { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<StudentAnswer> StudentAnswers { get; set; }
        public virtual DbSet<StudentWork> StudentWorks { get; set; }
        public virtual DbSet<StudentCourse> StudentCourses { get; set; }
    }
}
