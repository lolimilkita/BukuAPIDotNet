using System.Net;

namespace BukuAPI.Services.LogService
{
    public class Logger
    {
        public async Task Log(IPAddress? clientIp, HttpRequest req, string? res, string logMessage)
        {
            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory.ToString();
                string dirPath = basePath + @"\Log";
                string fileName = Path.Combine(basePath + @"\Log", "Log" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
                string reqUrl = $"{clientIp} {req.Scheme}://{req.Host}{req.Path}{req.QueryString}";
                string resCode = res ?? "";

                await WriteToLog(dirPath, fileName, reqUrl, resCode, logMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task WriteToLog(string dir, string file, string reqUrl, string resCode, string content)
        {
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(dir, file), true))
            {
                await outputFile.WriteLineAsync(string.Format("Logged on: {1} at: {2}{0}Request: {5}{0}ResponseCode: {6}{0}Message: {3} at {4}{0}--------------------{0}",
                          Environment.NewLine, DateTime.Now.ToLongDateString(),
                          DateTime.Now.ToLongTimeString(), content, DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss.fff"),
                          reqUrl, resCode));
            }
        }
    }
}
