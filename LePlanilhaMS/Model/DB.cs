using Microsoft.EntityFrameworkCore;
using System;

namespace LePlanilhaMS.Model
{
    public class DB : DbContext
    {
        public static string strcn = string.Empty;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(strcn);
        }

        public DbSet<Pais> Pais { get; set; }
        public DbSet<Estado> Estado { get; set; }

        public DbSet<Municipio> Municipio { get; set; }

        public DbSet<DadosMS> DadosMS {get;set;}
    }
}