using BukuAPI.Services.SqlService;

namespace BukuAPI.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly ISqlService _sqlService;

        public OrderService(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public List<object>? AddOrder(Order newOrder)
        {
            var alreadyOrder = _sqlService.GetData($"dbo.[Order] WHERE bukuId = {newOrder.BukuId}");
            if (alreadyOrder.Count > 0) { return null; }

            var buku = _sqlService.GetData($"Buku WHERE id = {newOrder.BukuId}");
            if (buku.Count < 1) { return null; }

            if(newOrder.Durasi == null) { return null; }
            DateTime dateReturn = DateTime.Now.AddDays((double)newOrder.Durasi);

            string query = string.Format($"INSERT INTO dbo.[Order](bukuId,nama,durasi,dateReturn,isOrder,created) VALUES" +
                    $"({newOrder.BukuId}, '{newOrder.Nama}', {newOrder.Durasi}, '{dateReturn}', " +
                    $"1, GETDATE())");
            string sqlExec = _sqlService.ExecuteQuery(query);
            if (sqlExec == "Failed") { return null; }
            var result = _sqlService.GetData($"dbo.[Order] WHERE bukuId {newOrder.BukuId}");
            return result;
        }

        public List<object>? DeleteOrder(int id)
        {
            var order = _sqlService.GetData($"dbo.[Order] WHERE id = {id}");
            if (order.Count < 1) { return null; }

            string query = string.Format($"DELETE dbo.[Order] WHERE id = {id}");
            string sqlExec = _sqlService.ExecuteQuery(query);
            if (sqlExec == "Failed") { return null; }

            var result = _sqlService.GetData("dbo.[Order]");
            return result;
        }

        public List<object>? GetAllOrders()
        {
            var result = _sqlService.GetData("dbo.[Order]");
            return result;
        }

        public object? GetSingleOrder(int id)
        {
            var result = _sqlService.GetData($"dbo.[Order] WHERE id = {id}");
            if (result.Count < 1) { return null; }
            return result;
        }

        public List<object>? UpdateOrder(int id, Order editOrder)
        {
            var order = _sqlService.GetData($"dbo.[Order] WHERE id = {id}");
            if (order.Count < 1) { return null; }

            string query = string.Format($"UPDATE dbo.[Order] SET" +
                    $"nama = '{editOrder.Nama}', dateReturn = '{editOrder.DateReturn}', isOrder = 0, " +
                    $"updated = GETDATE() WHERE id = {id}");
            string sqlExec = _sqlService.ExecuteQuery(query);
            if (sqlExec == "Failed") { return null; }

            var result = _sqlService.GetData("dbo.[Order]");
            return result;
        }
    }
}
