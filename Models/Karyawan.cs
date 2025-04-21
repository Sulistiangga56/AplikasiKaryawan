using System.ComponentModel.DataAnnotations;

namespace AplikasiKaryawanApp.Models
{
    public class Karyawan
    {
        public int Id { get; set; }

        // [Required(ErrorMessage = "Kode Karyawan tidak boleh kosong.")]
        // [MinLength(5, ErrorMessage = "Kode Karyawan tidak boleh kurang dari 5 digit.")]
        // [MaxLength(5, ErrorMessage = "Kode Karyawan tidak boleh lebih dari 5 digit.")]
        public required string KodeKaryawan { get; set; }

        // [Required(ErrorMessage = "Nama Karyawan tidak boleh kosong.")]
        public required string NamaKaryawan { get; set; }

        public bool Aktif { get; set; }

        // [Required(ErrorMessage = "Cabang tidak boleh kosong.")]
        public int CabangId { get; set; }
        public required Cabang Cabang { get; set; }

        public Perusahaan? Perusahaan => Cabang?.Perusahaan;
    }
}
