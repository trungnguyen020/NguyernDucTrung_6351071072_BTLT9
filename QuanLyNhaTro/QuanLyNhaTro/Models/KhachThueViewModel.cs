// Models/KhachThueViewModel.cs
namespace QuanLyNhaTro.Models
{
    public class KhachThueViewModel
    {
        public int MaKhach { get; set; }
        public string HoTen { get; set; } = "";
        public string SoDienThoai { get; set; } = "";
        public int MaPhong { get; set; }
        public string TenPhong { get; set; } = "";
        public decimal GiaThue { get; set; }
        public string QueQuan { get; set; } = "";
        public bool DaDangKyTamTru { get; set; }
        public int? MaTinh { get; set; }
    }
}