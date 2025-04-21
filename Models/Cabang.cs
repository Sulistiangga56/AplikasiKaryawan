namespace AplikasiKaryawanApp.Models
{
    public class Cabang
    {
        public int Id { get; set; }
        public required string NamaCabang { get; set; }
        public bool Aktif { get; set; }

        public int PerusahaanId { get; set; }
        public required Perusahaan Perusahaan { get; set; }

        public required ICollection<Karyawan> Karyawan { get; set; }
    }
}
