namespace BukuAPI.Services.SqlService
{
    public interface ISqlService
    {
        List<object> GetData(string query);
        string ExecuteQuery(string query);
    }
}
