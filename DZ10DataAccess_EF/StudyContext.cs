using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DZ10DataAccess_EF
{
    class StudyContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=ACERE5-521\SQLEXPRESS01;Initial Catalog=Course;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseLecturer>()
                .HasKey(cl => new { cl.CourseId, cl.LecturerId });
            modelBuilder.Entity<CourseLecturer>()
                .HasOne(cl => cl.Course)
                .WithMany(c => c.CourseLecturers)
                .HasForeignKey(cl => cl.CourseId);
            modelBuilder.Entity<CourseLecturer>()
                .HasOne(cl => cl.Lecturer)
                .WithMany(l => l.CourseLecturers)
                .HasForeignKey(cl => cl.LecturerId);


            modelBuilder.Entity<CourseStudent>()
                .HasKey(cl => new { cl.CourseId, cl.StudentId });
            modelBuilder.Entity<CourseStudent>()
                .HasOne(cl => cl.Course)
                .WithMany(c => c.CourseStudents)
                .HasForeignKey(cl => cl.CourseId);
            modelBuilder.Entity<CourseStudent>()
                .HasOne(cl => cl.Student)
                .WithMany(l => l.CourseStudents)
                .HasForeignKey(cl => cl.StudentId);


            //modelBuilder.Entity<HomeTask>()
            //    .HasOne(p => p.Course)
            //    .WithMany(b => b.HomeTasks)
            //    .HasForeignKey(s => new { s.Course });  
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<HomeTask> HomeTasks { get; set; }

        public DbSet<HomeTaskAssessment> HomeTaskAssessments { get; set; }

        public DbSet<Lecturer> Lecturers { get; set; }

        public DbSet<CourseStudent> CourseStudents { get; set; }

        public DbSet<CourseLecturer> CourseLecturers { get; set; }
    }
}
