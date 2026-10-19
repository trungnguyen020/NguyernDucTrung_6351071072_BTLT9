using Microsoft.EntityFrameworkCore;
using Serilog;
using QuanLyNhaTro.Configuration;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;
using System.Net.Http;

namespace QuanLyNhaTro
{
    public partial class FormKhachThue : Form
    {
        private List<ProvinceDto> _danhSachTinh = new();
        private int? _maKhachDangChon = null;

        public FormKhachThue()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        // ================== CONTEXT ==================
        private QL_NhaTroContext TaoContext()
        {
            var options = new DbContextOptionsBuilder<QL_NhaTroContext>()
                .UseSqlServer(ConfigHelper.ConnectionString)
                .Options;
            return new QL_NhaTroContext(options);
        }

        // ================== LOAD FORM ==================
        private async void FormKhachThue_Load(object? sender, EventArgs e)
        {
            KhoiTaoControlTinh();

            // Nạp dữ liệu CSDL trước để lưới hiện ngay, không phải chờ API tỉnh/thành
            NapComboBoxPhong();
            NapComboBoxLocPhong();
            TaiDanhSachKhach();

            // Nút ẩn gây lỗi thử: Ctrl+Shift+E để hiện/ẩn
            btnGayLoiThu.Visible = false;

            await NapDanhSachTinhAsync();
        }

        private void FormKhachThue_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.E)
            {
                btnGayLoiThu.Visible = !btnGayLoiThu.Visible;
            }
        }

        private void KhoiTaoControlTinh()
        {
            cboTinh.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPhuongXa.DropDownStyle = ComboBoxStyle.DropDownList;

            dtpNgaySinh.Value = new DateTime(2000, 1, 1);
            dtpNgaySinh.MaxDate = DateTime.Today;
            rdoNam.Checked = true;
        }

        // ================== GỌI API: NẠP DANH SÁCH TỈNH ==================
        private async Task NapDanhSachTinhAsync()
        {
            cboTinh.Enabled = false;
            cboTinh.DataSource = null;
            cboTinh.Items.Clear();
            cboTinh.Items.Add("Đang tải...");
            cboTinh.SelectedIndex = 0;

            try
            {
                _danhSachTinh = await ProvinceApiService.GetProvincesAsync();

                cboTinh.DropDownStyle = ComboBoxStyle.DropDownList; // khôi phục chế độ chọn
                cboTinh.DataSource = _danhSachTinh;
                cboTinh.DisplayMember = "Name";
                cboTinh.ValueMember = "Code";
                cboTinh.SelectedIndex = -1;

                lblStatus.Text = $"Đã tải {_danhSachTinh.Count} tỉnh/thành từ provinces.open-api.vn";
                lblStatus.ForeColor = Color.Green;

                Log.Information("Tải thành công {SoLuong} tỉnh/thành từ API", _danhSachTinh.Count);
            }
            catch (Exception ex) when (ex is HttpRequestException || ex is TaskCanceledException)
            {
                // Mất mạng / timeout -> không để app chết, cho phép gõ tay
                cboTinh.Items.Clear();
                cboTinh.DropDownStyle = ComboBoxStyle.DropDown; // cho gõ tay
                cboTinh.Text = "";

                lblStatus.Text = "Không tải được danh sách địa chỉ, vui lòng kiểm tra mạng";
                lblStatus.ForeColor = Color.Red;

                Log.Warning(ex, "Không tải được danh sách Tỉnh/Thành từ API (lỗi mạng/timeout)");

                MessageBox.Show(
                    "Không tải được danh sách địa chỉ, vui lòng kiểm tra mạng.\n" +
                    "Bạn vẫn có thể nhập tay Tỉnh/Thành và Phường/Xã để lưu thông tin khách.",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Lỗi không xác định khi tải danh sách Tỉnh/Thành");
                cboTinh.Items.Clear();
                cboTinh.DropDownStyle = ComboBoxStyle.DropDown;
                lblStatus.Text = "Lỗi khi tải danh sách địa chỉ";
                lblStatus.ForeColor = Color.Red;
            }
            finally
            {
                cboTinh.Enabled = true;
            }
        }

        // ================== GỌI API: NẠP DANH SÁCH PHƯỜNG/XÃ ==================
        private async void CboTinh_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cboTinh.SelectedValue == null || cboTinh.SelectedValue is not int maTinh)
                return;

            cboPhuongXa.Enabled = false;
            cboPhuongXa.DataSource = null;
            cboPhuongXa.Items.Clear();
            cboPhuongXa.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPhuongXa.Items.Add("Đang tải...");
            cboPhuongXa.SelectedIndex = 0;

            try
            {
                var wards = await ProvinceApiService.GetWardsAsync(maTinh);

                cboPhuongXa.DataSource = wards;
                cboPhuongXa.DisplayMember = "Name";
                cboPhuongXa.ValueMember = "Code";
                cboPhuongXa.SelectedIndex = -1;

                Log.Information("Tải {SoLuong} phường/xã cho tỉnh {MaTinh}", wards.Count, maTinh);
            }
            catch (Exception ex) when (ex is HttpRequestException || ex is TaskCanceledException)
            {
                cboPhuongXa.Items.Clear();
                cboPhuongXa.DropDownStyle = ComboBoxStyle.DropDown; // cho gõ tay
                cboPhuongXa.Text = "";

                Log.Warning(ex, "Không tải được danh sách Phường/Xã cho tỉnh {MaTinh}", maTinh);

                MessageBox.Show(
                    "Không tải được danh sách Phường/Xã, vui lòng kiểm tra mạng.\n" +
                    "Bạn có thể nhập tay vào ô Phường/Xã.",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Lỗi không xác định khi tải Phường/Xã của tỉnh {MaTinh}", maTinh);
                cboPhuongXa.Items.Clear();
                cboPhuongXa.DropDownStyle = ComboBoxStyle.DropDown;
            }
            finally
            {
                cboPhuongXa.Enabled = true;
            }
        }

        // ================== NẠP COMBOBOX PHÒNG ==================
        private void NapComboBoxPhong()
        {
            try
            {
                using var ctx = TaoContext();
                var dsPhong = ctx.PhongTros
                    .OrderBy(p => p.TenPhong)
                    .Select(p => new
                    {
                        p.MaPhong,
                        HienThi = $"{p.TenPhong} - {p.GiaThue:N0}đ ({p.TinhTrang})"
                    })
                    .ToList();

                cboPhong.DataSource = dsPhong;
                cboPhong.DisplayMember = "HienThi";
                cboPhong.ValueMember = "MaPhong";
                cboPhong.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Lỗi khi nạp danh sách phòng");
                MessageBox.Show("Không thể tải danh sách phòng. Vui lòng kiểm tra kết nối CSDL.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NapComboBoxLocPhong()
        {
            try
            {
                using var ctx = TaoContext();
                var dsPhong = ctx.PhongTros.OrderBy(p => p.TenPhong)
                    .Select(p => new { p.MaPhong, p.TenPhong }).ToList();

                var danhSach = new List<dynamic> { new { MaPhong = 0, TenPhong = "Tất cả" } };
                danhSach.AddRange(dsPhong);

                cboLocPhong.DataSource = danhSach;
                cboLocPhong.DisplayMember = "TenPhong";
                cboLocPhong.ValueMember = "MaPhong";
                cboLocPhong.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Lỗi khi nạp combobox lọc phòng");
            }
        }

        // ================== TẢI DANH SÁCH KHÁCH (JOIN) ==================
        private void TaiDanhSachKhach()
        {
            try
            {
                using var ctx = TaoContext();

                var query = ctx.KhachThues
                    .Include(k => k.MaPhongNavigation)
                    .AsNoTracking()
                    .AsQueryable();

                // Lọc theo tên/SĐT
                if (!string.IsNullOrWhiteSpace(txtTimKiem.Text))
                {
                    string key = txtTimKiem.Text.Trim().ToLower();
                    query = query.Where(k =>
                        k.HoTen.ToLower().Contains(key) ||
                        k.SoDienThoai.Contains(key));
                }

                // Lọc theo phòng
                if (cboLocPhong.SelectedValue is int maPhongLoc && maPhongLoc != 0)
                {
                    query = query.Where(k => k.MaPhong == maPhongLoc);
                }

                // Lọc theo tỉnh
                if (cboLocTinh.SelectedValue is int maTinhLoc && maTinhLoc != 0)
                {
                    query = query.Where(k => k.MaTinh == maTinhLoc);
                }

                var ketQua = query
                    .Select(k => new KhachThueViewModel
                    {
                        MaKhach = k.MaKhach,
                        HoTen = k.HoTen,
                        SoDienThoai = k.SoDienThoai,
                        MaPhong = k.MaPhong,
                        TenPhong = k.MaPhongNavigation.TenPhong,
                        GiaThue = k.MaPhongNavigation.GiaThue,
                        QueQuan = (k.TenPhuongXa ?? "") + " - " + (k.TenTinh ?? ""),
                        DaDangKyTamTru = k.DaDangKyTamTru,
                        MaTinh = k.MaTinh
                    })
                    .OrderBy(k => k.MaKhach)
                    .ToList();

                Log.Information("Đã nạp {SoLuong} khách thuê vào lưới", ketQua.Count);

                dgvKhachThue.AutoGenerateColumns = true;
                dgvKhachThue.DataSource = ketQua;
                DinhDangCot();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Lỗi khi nạp danh sách khách thuê");
                MessageBox.Show("Không thể tải danh sách khách thuê. Vui lòng kiểm tra kết nối CSDL.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DinhDangCot()
        {
            if (dgvKhachThue.Columns["MaKhach"] != null) dgvKhachThue.Columns["MaKhach"].Visible = false;
            if (dgvKhachThue.Columns["MaPhong"] != null) dgvKhachThue.Columns["MaPhong"].Visible = false;
            if (dgvKhachThue.Columns["MaTinh"] != null) dgvKhachThue.Columns["MaTinh"].Visible = false;

            if (dgvKhachThue.Columns["HoTen"] != null) dgvKhachThue.Columns["HoTen"].HeaderText = "Họ tên";
            if (dgvKhachThue.Columns["SoDienThoai"] != null) dgvKhachThue.Columns["SoDienThoai"].HeaderText = "SĐT";
            if (dgvKhachThue.Columns["TenPhong"] != null) dgvKhachThue.Columns["TenPhong"].HeaderText = "Phòng";
            if (dgvKhachThue.Columns["GiaThue"] != null)
            {
                dgvKhachThue.Columns["GiaThue"].HeaderText = "Giá thuê";
                dgvKhachThue.Columns["GiaThue"].DefaultCellStyle.Format = "N0";
            }
            if (dgvKhachThue.Columns["QueQuan"] != null) dgvKhachThue.Columns["QueQuan"].HeaderText = "Quê quán";
            if (dgvKhachThue.Columns["DaDangKyTamTru"] != null)
                dgvKhachThue.Columns["DaDangKyTamTru"].HeaderText = "Tạm trú";
        }

        // ================== CHỌN DÒNG -> ĐỔ LÊN CONTROL ==================
        private void dgvKhachThue_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvKhachThue.CurrentRow?.DataBoundItem is not KhachThueViewModel vm) return;

            try
            {
                using var ctx = TaoContext();
                var kt = ctx.KhachThues.AsNoTracking().FirstOrDefault(k => k.MaKhach == vm.MaKhach);
                if (kt == null) return;

                _maKhachDangChon = kt.MaKhach;
                txtHoTen.Text = kt.HoTen;
                rdoNam.Checked = kt.GioiTinh == "Nam";
                rdoNu.Checked = kt.GioiTinh == "Nữ";
                dtpNgaySinh.Value = kt.NgaySinh.ToDateTime(TimeOnly.MinValue);
                txtSDT.Text = kt.SoDienThoai;
                chkTamTru.Checked = kt.DaDangKyTamTru;

                cboPhong.SelectedValue = kt.MaPhong;

                if (kt.MaTinh.HasValue && cboTinh.Items.Count > 0 && cboTinh.DropDownStyle == ComboBoxStyle.DropDownList)
                    cboTinh.SelectedValue = kt.MaTinh.Value;
                else
                    cboTinh.Text = kt.TenTinh ?? "";

                if (kt.MaPhuongXa.HasValue && cboPhuongXa.DropDownStyle == ComboBoxStyle.DropDownList)
                    cboPhuongXa.SelectedValue = kt.MaPhuongXa.Value;
                else
                    cboPhuongXa.Text = kt.TenPhuongXa ?? "";
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Lỗi khi hiển thị chi tiết khách thuê MaKhach={MaKhach}", vm.MaKhach);
            }
        }

        // ================== VALIDATE ==================
        private bool KiemTraHopLe(out string loi)
        {
            loi = "";
            if (string.IsNullOrWhiteSpace(txtHoTen.Text)) { loi = "Họ tên không được để trống."; return false; }
            if (string.IsNullOrWhiteSpace(txtSDT.Text)) { loi = "Số điện thoại không được để trống."; return false; }
            if (dtpNgaySinh.Value.Date > DateTime.Today) { loi = "Ngày sinh không hợp lệ."; return false; }
            if (cboPhong.SelectedValue == null) { loi = "Vui lòng chọn phòng."; return false; }
            return true;
        }

        private void LamSachControl()
        {
            _maKhachDangChon = null;
            txtHoTen.Clear();
            rdoNam.Checked = true;
            dtpNgaySinh.Value = new DateTime(2000, 1, 1);
            txtSDT.Clear();
            cboPhong.SelectedIndex = -1;
            cboTinh.SelectedIndex = -1;
            cboPhuongXa.DataSource = null;
            cboPhuongXa.Items.Clear();
            chkTamTru.Checked = false;
            dgvKhachThue.ClearSelection();
        }

        // ================== CẬP NHẬT TRẠNG THÁI PHÒNG ==================
        private void CapNhatTrangThaiPhong(QL_NhaTroContext ctx, int maPhong)
        {
            var phong = ctx.PhongTros.FirstOrDefault(p => p.MaPhong == maPhong);
            if (phong == null) return;

            bool conKhach = ctx.KhachThues.Any(k => k.MaPhong == maPhong);
            phong.TinhTrang = conKhach ? "Đang thuê" : "Trống";
        }

        // ================== THÊM ==================
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!KiemTraHopLe(out string loi))
            {
                MessageBox.Show(loi, "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var ctx = TaoContext();

                var khach = new KhachThue
                {
                    HoTen = txtHoTen.Text.Trim(),
                    GioiTinh = rdoNam.Checked ? "Nam" : "Nữ",
                    NgaySinh = DateOnly.FromDateTime(dtpNgaySinh.Value.Date),
                    SoDienThoai = txtSDT.Text.Trim(),
                    MaPhong = (int)cboPhong.SelectedValue!,
                    MaTinh = cboTinh.DropDownStyle == ComboBoxStyle.DropDownList && cboTinh.SelectedValue is int mt ? mt : null,
                    TenTinh = cboTinh.Text,
                    MaPhuongXa = cboPhuongXa.DropDownStyle == ComboBoxStyle.DropDownList && cboPhuongXa.SelectedValue is int mp ? mp : null,
                    TenPhuongXa = cboPhuongXa.Text,
                    DaDangKyTamTru = chkTamTru.Checked
                };

                ctx.KhachThues.Add(khach);
                ctx.SaveChanges();

                CapNhatTrangThaiPhong(ctx, khach.MaPhong);
                ctx.SaveChanges();

                Log.Information("Thêm khách thuê thành công. MaKhach={MaKhach}, MaPhong={MaPhong}",
                    khach.MaKhach, khach.MaPhong);

                MessageBox.Show("Thêm khách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LamSachControl();
                NapComboBoxPhong();
                NapComboBoxLocPhong();
                TaiDanhSachKhach();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Lỗi khi thêm khách thuê");
                MessageBox.Show("Không thể thêm khách. Vui lòng thử lại sau.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================== SỬA ==================
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_maKhachDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn một khách trong danh sách để sửa.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!KiemTraHopLe(out string loi))
            {
                MessageBox.Show(loi, "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var ctx = TaoContext();
                var khach = ctx.KhachThues.FirstOrDefault(k => k.MaKhach == _maKhachDangChon);
                if (khach == null)
                {
                    MessageBox.Show("Khách không còn tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TaiDanhSachKhach();
                    return;
                }

                int maPhongCu = khach.MaPhong;
                int maPhongMoi = (int)cboPhong.SelectedValue!;

                khach.HoTen = txtHoTen.Text.Trim();
                khach.GioiTinh = rdoNam.Checked ? "Nam" : "Nữ";
                khach.NgaySinh = DateOnly.FromDateTime(dtpNgaySinh.Value.Date);
                khach.SoDienThoai = txtSDT.Text.Trim();
                khach.MaPhong = maPhongMoi;
                khach.MaTinh = cboTinh.DropDownStyle == ComboBoxStyle.DropDownList && cboTinh.SelectedValue is int mt ? mt : null;
                khach.TenTinh = cboTinh.Text;
                khach.MaPhuongXa = cboPhuongXa.DropDownStyle == ComboBoxStyle.DropDownList && cboPhuongXa.SelectedValue is int mp ? mp : null;
                khach.TenPhuongXa = cboPhuongXa.Text;
                khach.DaDangKyTamTru = chkTamTru.Checked;

                ctx.SaveChanges();

                // Cập nhật trạng thái cho cả phòng cũ (nếu đổi phòng) và phòng mới
                CapNhatTrangThaiPhong(ctx, maPhongCu);
                if (maPhongMoi != maPhongCu)
                    CapNhatTrangThaiPhong(ctx, maPhongMoi);
                ctx.SaveChanges();

                Log.Information("Sửa khách thuê thành công. MaKhach={MaKhach}", khach.MaKhach);
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LamSachControl();
                NapComboBoxPhong();
                NapComboBoxLocPhong();
                TaiDanhSachKhach();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Lỗi khi sửa khách thuê MaKhach={MaKhach}", _maKhachDangChon);
                MessageBox.Show("Không thể cập nhật. Vui lòng thử lại sau.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================== XÓA ==================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_maKhachDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn một khách trong danh sách để xóa.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var xacNhan = MessageBox.Show("Bạn có chắc chắn muốn xóa khách này không?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (xacNhan != DialogResult.Yes) return;

            try
            {
                using var ctx = TaoContext();
                var khach = ctx.KhachThues.FirstOrDefault(k => k.MaKhach == _maKhachDangChon);
                if (khach == null)
                {
                    MessageBox.Show("Khách không còn tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TaiDanhSachKhach();
                    return;
                }

                int maKhach = khach.MaKhach;
                int maPhong = khach.MaPhong;

                ctx.KhachThues.Remove(khach);
                ctx.SaveChanges();

                // Nếu đây là khách cuối cùng của phòng -> phòng trở lại "Trống"
                CapNhatTrangThaiPhong(ctx, maPhong);
                ctx.SaveChanges();

                Log.Information("Xóa khách thuê thành công. MaKhach={MaKhach}, MaPhong={MaPhong}", maKhach, maPhong);
                MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LamSachControl();
                NapComboBoxPhong();
                NapComboBoxLocPhong();
                TaiDanhSachKhach();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Lỗi khi xóa khách thuê MaKhach={MaKhach}", _maKhachDangChon);
                MessageBox.Show("Không thể xóa. Vui lòng thử lại sau.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================== LÀM MỚI ==================
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            cboLocPhong.SelectedIndex = 0;
            if (cboLocTinh.Items.Count > 0) cboLocTinh.SelectedIndex = 0;
            LamSachControl();
            TaiDanhSachKhach();
        }

        // ================== TÌM KIẾM / LỌC ==================
        private void btnTim_Click(object sender, EventArgs e) => TaiDanhSachKhach();
        private void txtTimKiem_TextChanged(object sender, EventArgs e) => TaiDanhSachKhach();
        private void cboLocPhong_SelectedIndexChanged(object sender, EventArgs e) => TaiDanhSachKhach();
        private void cboLocTinh_SelectedIndexChanged(object sender, EventArgs e) => TaiDanhSachKhach();

        private void btnBoLoc_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            cboLocPhong.SelectedIndex = 0;
            if (cboLocTinh.Items.Count > 0) cboLocTinh.SelectedIndex = 0;
            TaiDanhSachKhach();
        }

        // ================== NÚT ẨN: GÂY LỖI THỬ (Task 9.3) ==================
        private void btnGayLoiThu_Click(object sender, EventArgs e)
        {
            // Cố ý ném lỗi để kiểm tra cơ chế bắt lỗi toàn cục
            throw new InvalidOperationException(
                "Đây là lỗi THỬ NGHIỆM được tạo từ nút 'Gây lỗi thử' (Ctrl+Shift+E để hiện nút).");
        }
    }
}