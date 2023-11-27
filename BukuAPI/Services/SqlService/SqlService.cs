using System.Data.Common;
using System.Data.SqlClient;

namespace BukuAPI.Services.SqlService
{
    public class SqlService : ISqlService
    {
        public SqlConnectionStringBuilder _builder = new SqlConnectionStringBuilder()
        {
            DataSource = "DESKTOP-DJKEFR0",
            InitialCatalog = "BukuApi",
            UserID = "catur",
            Password = "rahasiabgt",
            IntegratedSecurity = true
        };

        public string ExecuteQuery(string query)
        {
            Task<string> task = PerformDbExecute(_builder.ConnectionString, query);
            task.Wait();

            return task.Result;
        }

        private static async Task<string> PerformDbExecute(string connectionString, string query)
        {
            try
            {
                using DbConnection connection = SqlClientFactory.Instance.CreateConnection();
                connection.ConnectionString = connectionString;
                await connection.OpenAsync();

                DbCommand command = connection.CreateCommand();
                command.CommandText = query;

                await command.ExecuteNonQueryAsync();
                return "Success";
            }
            catch(Exception ex) 
            {
                return "Failed";
            }
        }

        public List<object> GetData(string query)
        {
            Task<List<object>> task = PerformDbGetData(_builder.ConnectionString, query);
            task.Wait();

            return task.Result;
        }

        private static async Task<List<object>> PerformDbGetData(string connectionString, string query)
        {
            List<object> data = new();

            try
            {
                using DbConnection connection = SqlClientFactory.Instance.CreateConnection();
                connection.ConnectionString = connectionString;
                await connection.OpenAsync();

                DbCommand command = connection.CreateCommand();
                command.CommandText = $"SELECT * FROM {query}";

                using DbDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    if (query.Contains("Buku"))
                    {
                        var test = reader["created"];

                        var ssdsd = reader["created"].GetType;

                        Buku record = new()
                        {
                            Id = (int)reader["id"],
                            JudulBuku = reader["judulBuku"] == DBNull.Value ? null : (string)reader["judulBuku"],
                            Penerbit = reader["penerbit"] == DBNull.Value ? null : (string)reader["penerbit"],
                            TahunTerbit = reader["tahunTerbit"] == DBNull.Value ? null : (string)reader["tahunTerbit"],
                            Gambar = reader["gambar"] == DBNull.Value ? null : (string)reader["gambar"],
                            Created = reader["created"] == DBNull.Value ? null : (DateTime)reader["created"],
                            Updated = reader["updated"] == DBNull.Value ? null : (DateTime)reader["updated"],
                        };
                        data.Add(record);
                    }
                    else if (query.Contains("Order"))
                    {
                        Order record = new()
                        {
                            Id = (int)reader["id"],
                            BukuId = (int)reader["bukuId"],
                            Nama = reader["nama"] == DBNull.Value ? null : (string)reader["nama"],
                            Durasi = (Int16)reader["durasi"],
                            DateReturn = (DateTime)reader["dateReturn"],
                            IsOrder = (bool)reader["isOrder"],
                            Created = reader["created"] == DBNull.Value ? null : (DateTime)reader["created"],
                            Updated = reader["updated"] == DBNull.Value ? null : (DateTime)reader["updated"],
                        };
                        data.Add(record);
                    }

                }
            }
            catch(Exception ex) 
            {
                
            }

            return data;
        }
    }
}
