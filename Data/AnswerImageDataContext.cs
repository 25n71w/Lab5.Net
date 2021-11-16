using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Lab5.NET.Data
{
    public class AnswerImageDataContext : DbContext
    {
        public AnswerImageDataContext (DbContextOptions<AnswerImageDataContext> options)
            : base(options)
        {

        }

        public DbSet<Lab5.NET.Models.AnswerImage> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lab5.NET.Models.AnswerImage>().ToTable("AnswerImage");
        }
    }
}
