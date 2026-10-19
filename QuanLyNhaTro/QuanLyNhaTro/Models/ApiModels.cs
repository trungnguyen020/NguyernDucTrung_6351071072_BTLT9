// Models/ApiModels.cs
using System.Text.Json.Serialization;

namespace QuanLyNhaTro.Models
{
    public class ProvinceDto
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("wards")]
        public List<WardDto>? Wards { get; set; }
    }

    public class WardDto
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
    }
}