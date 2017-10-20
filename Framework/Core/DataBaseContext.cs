﻿using Framework.Model;
using Microsoft.EntityFrameworkCore;

namespace Framework.Core
{
    public class DataBaseContext : DbContext
    {
        //public DataBaseContext(DbContextOptions<DataBaseContext> options) 
        //    : base(options)
        //{
        //}

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
        }
    }
}
