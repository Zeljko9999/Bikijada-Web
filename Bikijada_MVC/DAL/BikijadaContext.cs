using Bikijada_MVC.Models;
using Microsoft.EntityFrameworkCore;



namespace Bikijada_MVC.DAL
{
    public class BikijadaContext : DbContext
    {
        public DbSet<Vlasnik> Vlasnik { get; set; }
        public DbSet<Bik> Bik { get; set; }
        public DbSet<Pivac> Pivac { get; set; }
        public DbSet<Borba> Borba { get; set; }
        public DbSet<Oklada> Oklada { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder
        optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database" +
            "= BikijadaMVC; Integrated Security = True; Trusted_Connection =" +
            "true; MultipleActiveResultSets = True");
        }

    }
}