using Microsoft.Extensions.Configuration;

namespace QuanLyNhaTro.Configuration
{
    public static class ConfigHelper
    {
        private static IConfigurationRoot? _config;

        public static IConfigurationRoot Config
        {
            get
            {
                if (_config == null)
                {
                    _config = new ConfigurationBuilder()
                        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                        .AddJsonFile("appsettings.json", optional: false)
                        .Build();
                }
                return _config;
            }
        }

        public static string ConnectionString =>
            Config.GetConnectionString("QLNhaTro") ?? throw new Exception("Thiếu connection string QLNhaTro");
    }
}