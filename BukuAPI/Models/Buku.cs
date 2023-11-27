namespace BukuAPI.Models
{
    public class Buku
    {
        public int Id { get; set; }
        public string? JudulBuku { get; set; }
        public string? Penerbit { get; set; }
        public string? Penulis { get; set; }
        public string? TahunTerbit { get; set; }
        public string? Gambar { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
