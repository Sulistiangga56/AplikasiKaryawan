namespace AplikasiKaryawanApp.Models
{
    public class Perusahaan
    {
        public int Id { get; set; }
        public required string NamaPerusahaan { get; set; }
        public bool Aktif { get; set; }

        public required ICollection<Cabang> Cabang { get; set; }
    }
}
