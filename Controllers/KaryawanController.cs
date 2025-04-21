using Microsoft.AspNetCore.Mvc;
using AplikasiKaryawanApp.Models;
using AplikasiKaryawanApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AplikasiKaryawanApp.Controllers
{
    public class KaryawanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KaryawanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Karyawan
        public IActionResult Index()
        {
            var karyawanList = _context.Karyawan
                .Where(k => k.Aktif == true)
                .Include(k => k.Cabang)
                .ThenInclude(c => c.Perusahaan)
                .ToList();

            return View(karyawanList);
        }

        // GET: Karyawan/Create
        public IActionResult Create()
        {
            ViewBag.CabangId = new SelectList(
            _context.Cabang,
            "Id",
            "NamaCabang");

            return View();
        }

        // POST: Karyawan/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Karyawan karyawan)
        {
            karyawan.Aktif = true;
            _context.Karyawan.Add(karyawan);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Data Berhasil Dibuat.";
            return RedirectToAction("Edit", new { id = karyawan.Id });
        }

        [HttpGet]
        public IActionResult CekKodeKaryawan(string kode)
        {
            var exists = _context.Karyawan.Any(k => k.KodeKaryawan == kode && k.Aktif == true);
            return Json(!exists);
        }

        // GET: Karyawan/Edit
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var karyawan = _context.Karyawan
                .Include(k => k.Cabang)
                .FirstOrDefault(k => k.Id == id);

            if (karyawan == null)
            {
                return NotFound();
            }

            ViewBag.CabangId = new SelectList(
                _context.Cabang,
                "Id",
                "NamaCabang",
                karyawan.CabangId);

            return View(karyawan);
        }

        // POST: Karyawan/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Karyawan karyawan)
        {
            if (id != karyawan.Id)
            {
                return NotFound();
            }

            var existingKaryawan = _context.Karyawan.Find(id);
            if (existingKaryawan == null)
            {
                return NotFound();
            }

            existingKaryawan.NamaKaryawan = karyawan.NamaKaryawan;
            existingKaryawan.KodeKaryawan = karyawan.KodeKaryawan;
            existingKaryawan.CabangId = karyawan.CabangId;
            existingKaryawan.Aktif = karyawan.Aktif;

            _context.SaveChanges();
            TempData["SuccessMessage"] = "Data Berhasil Diupdate.";
            return RedirectToAction("Edit", new { id = existingKaryawan.Id });
        }

        // GET: Karyawan/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromBody] DeleteRequest request)
        {
            var karyawan = _context.Karyawan.Find(request.Id);
            if (karyawan != null)
            {
                karyawan.Aktif = false;
                _context.SaveChanges();

                return Json(new { success = true, message = $"Karyawan dengan kode '{karyawan.KodeKaryawan}' telah dinonaktifkan." });
            }
            else
            {
                return Json(new { success = false, message = "Data karyawan tidak ditemukan." });
            }
        }

        public class DeleteRequest
        {
            public int Id { get; set; }
        }
    }
}
