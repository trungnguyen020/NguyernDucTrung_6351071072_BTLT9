// Services/ProvinceApiService.cs
using System.Net.Http;
using System.Text.Json;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Services
{
    /// <summary>
    /// Service gọi API hành chính Việt Nam (provinces.open-api.vn).
    /// Dùng 1 HttpClient static dùng chung cho toàn ứng dụng (tránh socket exhaustion).
    /// </summary>
    public static class ProvinceApiService
    {
        private static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://provinces.open-api.vn/api/v2/"),
            Timeout = TimeSpan.FromSeconds(10)
        };

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        /// <summary>Lấy danh sách tất cả Tỉnh/Thành</summary>
        public static async Task<List<ProvinceDto>> GetProvincesAsync()
        {
            string json = await _httpClient.GetStringAsync("");
            var list = JsonSerializer.Deserialize<List<ProvinceDto>>(json, _jsonOptions);
            return list ?? new List<ProvinceDto>();
        }

        /// <summary>Lấy danh sách Phường/Xã trực thuộc 1 tỉnh (depth=2)</summary>
        public static async Task<List<WardDto>> GetWardsAsync(int provinceCode)
        {
            string json = await _httpClient.GetStringAsync($"p/{provinceCode}?depth=2");
            var province = JsonSerializer.Deserialize<ProvinceDto>(json, _jsonOptions);
            return province?.Wards ?? new List<WardDto>();
        }
    }
}