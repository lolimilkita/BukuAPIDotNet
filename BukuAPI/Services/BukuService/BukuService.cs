using BukuAPI.Services.SqlService;
using System.Linq;

namespace BukuAPI.Services.BukuService
{
    public class BukuService : IBukuService
    {

        private readonly ISqlService _sqlService;

        public BukuService(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public List<object>? AddBuku(Buku newBuku)
        {
            string query = string.Format($"INSERT INTO Buku(judulBuku,penerbit,penulis,tahunTerbit,gambar,created) VALUES" +
                    $"('{newBuku.JudulBuku}', '{newBuku.Penerbit}', '{newBuku.Penulis}', '{newBuku.TahunTerbit}', " +
                    $"'{newBuku.Gambar}', GETDATE())");
            string sqlExec = _sqlService.ExecuteQuery(query);
            if(sqlExec == "Failed") { return null; }
            var result = _sqlService.GetData("Buku");
            return result;
        }

        public List<object>? DeleteBuku(int id)
        {
            var buku = _sqlService.GetData($"Buku WHERE id = {id}");
            if (buku.Count < 1) { return null; }

            string query = string.Format($"DELETE Buku WHERE id = {id}");
            string sqlExec = _sqlService.ExecuteQuery(query);
            if (sqlExec == "Failed") { return null; }

            var result = _sqlService.GetData("Buku");
            return result;
        }

        public List<object>? GetAllBukus(string? search)
        {
            var result = _sqlService.GetData("Buku");

            if (search != null) 
            {
                if (search.ToUpper().Contains("DELETE") || search.ToUpper().Contains("TRUNCATE") ||
                search.ToUpper().Contains("UPDATE") || search.ToUpper().Contains("Id") || search.ToUpper().Contains("INSERT")) { return result; }
                result = _sqlService.GetData($"Buku WHERE judulBuku like '%{search}%'");
                if(result.Count > 0) 
                {
                    string bukuId = "";
                    foreach(Buku item in result) { if (result.Last() == item) { bukuId += item.Id; } else { bukuId += item.Id + ","; } }
                    var orderDetail = _sqlService.GetData($"dbo.[Order] WHERE bukuId in({bukuId}) ");
                    List<object> anu = GetJoin(result, orderDetail);
                    return anu;
                }

            }
            return result;
        }

        private List<object> GetJoin(List<object> buku, List<object> orderDetail)
        {
            var anu = from Buku n in buku
                         join Order c in orderDetail on n.Id equals c.BukuId into ps_jointable
                         from p in ps_jointable.DefaultIfEmpty()
                         select new
                         {
                             n.Id,
                             n.JudulBuku,
                             n.Penerbit,
                             n.Penulis,
                             n.TahunTerbit,
                             n.Gambar,
                             NamaPeminjam = p == null ? "No Order" : p.Nama,
                             Durasi = p == null ? "No Order" : p.Durasi.ToString(),
                             Status = p == null ? "No Order" : "Tidak Tersedia",
                             DikembalikanPada = p == null ? "No Order" : p.DateReturn.ToString(),
                         };

            List<object> result = new();

            foreach (var order in anu) 
            {
                JoinOrder record = new()
                {
                    Id = order.Id,
                    JudulBuku = order.JudulBuku,
                    Penerbit = order.Penerbit,
                    Penulis = order.Penulis,
                    TahunTerbit = order.TahunTerbit,
                    Gambar = order.Gambar,
                    NamaPeminjam = order.NamaPeminjam,
                    Durasi = order.Durasi,
                    Status = order.Status,
                    DikembalikanPada = order.DikembalikanPada,
                };
                result.Add(record);
            }

            return result;
        }

        public object? GetSingleBuku(int id)
        {
            var result = _sqlService.GetData($"Buku WHERE id = {id}");
            if(result.Count < 1) { return null; }
            return result;
        }

        public List<object>? UpdateBuku(int id, Buku editBuku)
        {
            var buku = _sqlService.GetData($"Buku WHERE id = {id}");
            if (buku.Count < 1) { return null; }

            string query = string.Format($"UPDATE Buku SET" +
                    $"judulBuku = '{editBuku.JudulBuku}', penerbit = '{editBuku.Penerbit}', penulis = '{editBuku.Penulis}', tahunTerbit = '{editBuku.TahunTerbit}', " +
                    $"gambar = '{editBuku.Gambar}', updated = GETDATE() WHERE id = {id}");
            string sqlExec = _sqlService.ExecuteQuery(query);
            if (sqlExec == "Failed") { return null; }
            
            var result = _sqlService.GetData("Buku");
            return result;
        }
    }
}
