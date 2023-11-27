namespace BukuAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int BukuId { get; set; }
        public string? Nama { get; set; }
        public Int16? Durasi { get; set; }
        public DateTime? DateReturn { get; set; }
        public Boolean IsOrder { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
