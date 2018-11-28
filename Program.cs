using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace App1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    [Table("Model")]
    public class Model1
    {
        public int Id { get; set; }
        public string Value1 { get; set; }
    }

    [Table("Model", Schema = "other")]
    public class Model2
    {
        public int Id { get; set; }
        public string Value2 { get; set; }
        public Model1 Model1 { get; set; }
    }

    public class AppContext : DbContext
    {
        public DbSet<Model2> Models { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            builder.UseNpgsql("Host=localhost; Database=test; Username=postgres;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Model2>()
                .HasOne(m => m.Model1)
                .WithMany()
                .HasForeignKey(m => m.Id);
        }
    }
}
