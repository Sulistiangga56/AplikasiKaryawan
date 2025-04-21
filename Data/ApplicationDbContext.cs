using Microsoft.EntityFrameworkCore;
using AplikasiKaryawanApp.Models;

namespace AplikasiKaryawanApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet untuk masing-masing model
        public DbSet<Perusahaan> Perusahaan { get; set; }
        public DbSet<Cabang> Cabang { get; set; }
        public DbSet<Karyawan> Karyawan { get; set; }
    }
}
