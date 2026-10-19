using Serilog;
using QuanLyNhaTro.Logging;

namespace QuanLyNhaTro
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            LogConfig.Init();
            Log.Information("===== Ứng dụng Quản lý nhà trọ khởi động =====");

            // --- Đăng ký bắt lỗi toàn cục (quan trọng: phải gọi TRƯỚC khi Application.Run) ---
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            ApplicationConfiguration.Initialize();

            try
            {
                Application.Run(new FormKhachThue());
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Lỗi nghiêm trọng khi chạy ứng dụng");
                MessageBox.Show("Đã xảy ra lỗi, thông tin đã được ghi lại.",
                    "Lỗi nghiêm trọng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Log.Information("===== Ứng dụng đóng =====");
                Log.CloseAndFlush();
            }
        }

        // Bắt lỗi xảy ra trên UI Thread (WinForms message loop)
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Log.Error(e.Exception, "Lỗi UI Thread chưa được xử lý");
            MessageBox.Show(
                "Đã xảy ra lỗi, thông tin đã được ghi lại.\nỨng dụng vẫn tiếp tục hoạt động.",
                "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // Bắt lỗi xảy ra trên các Thread khác (background thread, Task...)
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            Log.Fatal(ex, "Lỗi nghiêm trọng chưa được xử lý (AppDomain)");
            MessageBox.Show(
                "Đã xảy ra lỗi nghiêm trọng, thông tin đã được ghi lại.",
                "Lỗi nghiêm trọng", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}