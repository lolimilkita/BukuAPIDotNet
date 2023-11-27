namespace BukuAPI.Models
{
    public class JoinOrder
    {
        public int Id { get; set; }
        public string? JudulBuku { get; set; }
        public string? Penerbit { get; set; }
        public string? Penulis { get; set; }
        public string? TahunTerbit { get; set; }
        public string? Gambar { get; set; }
        public string? NamaPeminjam { get; set; }
        public string? Durasi { get; set; }
        public string? Status { get; set; }
        public string? DikembalikanPada { get; set; }
    }
}
