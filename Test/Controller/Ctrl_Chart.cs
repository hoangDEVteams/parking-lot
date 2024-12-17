using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.DataVisualization.Charting;

namespace Test.Controller
{
    public class Ctrl_Chart
    {
        public class RevenueData
        {
            public string Month { get; set; }
            public decimal TotalRevenue { get; set; }
        }
        public List<RevenueData> GetRevenueData()
        {
            DateTime threeMonthsAgo = DateTime.Now.AddMonths(-3);
            var rawData = CUltils.db.Rentals
                .Where(r => r.RentalDate >= threeMonthsAgo)
                .Join(
                    CUltils.db.RentalDetails,
                    rental => rental.IDRental,
                    detail => detail.IDRental,
                    (rental, detail) => new
                    {
                        rental.RentalDate,
                        Revenue = detail.RentalDays * detail.RentPrice
                    }
                )
                .GroupBy(x => new { Year = x.RentalDate.Year, Month = x.RentalDate.Month })
                .Select(g => new
                {
                    Month = g.Key.Month,
                    Year = g.Key.Year,
                    TotalRevenue = g.Sum(x => x.Revenue)
                })
                .OrderBy(x => x.Year).ThenBy(x => x.Month)
                .ToList();
            
            var revenueData = rawData.Select(x => new RevenueData
            {
                Month = $"{x.Month}/{x.Year}",
                TotalRevenue = x.TotalRevenue
            }).ToList();

            return revenueData;
        }


        public static string GenerateAndSendRevenueChart()
        {
            Ctrl_Chart chartController = new Ctrl_Chart();
            List<Ctrl_Chart.RevenueData> revenueData = chartController.GetRevenueData();

            if (revenueData != null && revenueData.Count > 0)
            {
                string chartPath = Ctrl_Chart.GenerateRevenueChart(revenueData);
                Console.WriteLine($"Biểu đồ đã được lưu tại: {chartPath}");
                return chartPath;
            }
            else
            {
                Console.WriteLine("Không có dữ liệu để tạo biểu đồ.");
                return null;
            }
        }




        public static string GenerateRevenueChart(List<RevenueData> revenueData)
        {
            // Tạo đối tượng Chart
            Chart chart = new Chart
            {
                Width = 600,
                Height = 400
            };

            ChartArea chartArea = new ChartArea("RevenueChartArea");
            chart.ChartAreas.Add(chartArea);

            // Tạo Series
            Series series = new Series("Doanh Thu")
            {
                ChartType = SeriesChartType.Column
            };

            foreach (var data in revenueData)
            {
                series.Points.AddXY(data.Month, (double)data.TotalRevenue);
            }

            chart.Series.Add(series);

            // Tùy chỉnh giao diện
            chartArea.AxisX.Title = "Tháng";
            chartArea.AxisY.Title = "Doanh Thu (VND)";
            chartArea.AxisX.Interval = 1;
            chart.Titles.Add("Thống Kê Doanh Thu 3 Tháng Gần Nhất");

            // Đường dẫn lưu hình ảnh
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string resourceFolder = Path.Combine(baseDirectory, "Resource", "img");

            if (!Directory.Exists(resourceFolder))
            {
                Directory.CreateDirectory(resourceFolder);
            }

            string filePath = Path.Combine(resourceFolder, "Chart.png");
            chart.SaveImage(filePath, ChartImageFormat.Png);

            return filePath;
        }

        public static string GenerateBarChart()
        {
            // Tạo đối tượng Chart
            Chart chart = new Chart
            {
                Width = 600,
                Height = 400
            };

            ChartArea chartArea = new ChartArea("MainArea");
            chart.ChartAreas.Add(chartArea);

            // Tạo Series
            Series series = new Series("Thống Kê")
            {
                ChartType = SeriesChartType.Column
            };

            // Thêm dữ liệu mẫu
            series.Points.AddXY("Tháng 1", 100);
            series.Points.AddXY("Tháng 2", 200);
            series.Points.AddXY("Tháng 3", 150);
            series.Points.AddXY("Tháng 4", 300);

            chart.Series.Add(series);

            // Đường dẫn tương đối tới folder img
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory; 
            string resourceFolder = Path.Combine(baseDirectory, "Resource", "img");
            if (!Directory.Exists(resourceFolder))
            {
                Directory.CreateDirectory(resourceFolder);
            }

            string filePath = Path.Combine(resourceFolder, "chart.png");
            chart.SaveImage(filePath, ChartImageFormat.Png);

            return filePath;
        }
        public class UserStatistics
        {
            public string UserType { get; set; } // Customer hoặc Employee
            public int Count { get; set; }
        }
        public class UserStatisticsByMonth
        {
            public string UserType { get; set; }
            public int Month { get; set; }
            public int Year { get; set; }
            public int Count { get; set; }
        }
        
        public void GenerateAndSendUserComparisonChart(string recipientEmail)
        {
            // Lấy dữ liệu thống kê
            List<UserStatisticsByMonth> statistics = GetUserStatisticsForLastSixMonths();

            if (statistics == null || statistics.Count == 0)
            {
                Console.WriteLine("Không có dữ liệu để tạo biểu đồ.");
                return;
            }

            // Tạo biểu đồ
            string chartPath = GenerateUserComparisonChart(statistics);

            // Gửi email
            Ctrl_Account.SendEmailWithAttachment(recipientEmail, chartPath);
            Console.WriteLine($"Biểu đồ thống kê người dùng 6 tháng qua đã được gửi tới {recipientEmail}");
        }

        public string GenerateUserComparisonChart(List<UserStatisticsByMonth> statistics)
        {
            // Tạo đối tượng Chart
            Chart chart = new Chart
            {
                Width = 800,
                Height = 400
            };

            ChartArea chartArea = new ChartArea("UserComparisonArea");
            chart.ChartAreas.Add(chartArea);

            // Tạo Series cho Customer và Employee
            Series customerSeries = new Series("Customer")
            {
                ChartType = SeriesChartType.Column
            };

            Series employeeSeries = new Series("Employee")
            {
                ChartType = SeriesChartType.Column
            };

            // Thêm dữ liệu vào Series
            var groupedData = statistics.GroupBy(s => new { s.Year, s.Month });

            foreach (var group in groupedData)
            {
                string label = $"{group.Key.Month}/{group.Key.Year}";

                int customerCount = group.Where(s => s.UserType == "Khách Hàng").Sum(s => s.Count);
                int employeeCount = group.Where(s => s.UserType == "Nhân Viên").Sum(s => s.Count);

                customerSeries.Points.AddXY(label, customerCount);
                employeeSeries.Points.AddXY(label, employeeCount);
            }

            chart.Series.Add(customerSeries);
            chart.Series.Add(employeeSeries);

            // Tùy chỉnh giao diện biểu đồ
            chartArea.AxisX.Title = "Tháng";
            chartArea.AxisY.Title = "Số Lượng Người Dùng";
            chartArea.AxisX.Interval = 1;
            chart.Titles.Add("Thống Kê Người Dùng Trong 6 Tháng Qua");

            // Đường dẫn lưu biểu đồ
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string resourceFolder = Path.Combine(baseDirectory, "Resource", "img");

            if (!Directory.Exists(resourceFolder))
            {
                Directory.CreateDirectory(resourceFolder);
            }

            string filePath = Path.Combine(resourceFolder, "UserComparisonChart.png");
            chart.SaveImage(filePath, ChartImageFormat.Png);

            return filePath;
        }
        public static List<UserStatistics> GetUserStatisticsForCurrentMonth()
        {
            DateTime startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            return CUltils.db.Users
                .Join(CUltils.db.Accounts,
                      user => user.IDUser,
                      account => account.IDUser,
                      (user, account) => new { user, account })
                .Where(u => u.user.UserType == "Khách Hàng" || u.user.UserType == "Nhân Viên")
                .Where(u => u.account.VerificationCodeExpiration.HasValue &&
                            u.account.VerificationCodeExpiration.Value.Month == currentMonth &&
                            u.account.VerificationCodeExpiration.Value.Year == currentYear)
                .GroupBy(u => u.user.UserType)
                .Select(group => new UserStatistics
                {
                    UserType = group.Key,
                    Count = group.Count()
                })
                .ToList();
        }



        public string GetUserStatisticsSummary()
        {
            List<UserStatistics> statistics = GetUserStatisticsForCurrentMonth();

            if (statistics == null || statistics.Count == 0)
                return "Không có dữ liệu thống kê cho tháng này.";

            string summary = "Thống kê người dùng tháng này:\n";
            foreach (var stat in statistics)
            {
                summary += $"{stat.UserType}: {stat.Count}\n";
            }

            return summary.Trim();
        }
        public List<UserStatisticsByMonth> GetUserStatisticsForLastSixMonths()
        {
            DateTime sixMonthsAgo = DateTime.Now.AddMonths(-6);

            return CUltils.db.Users
                .Join(CUltils.db.Accounts,
                      user => user.IDUser,
                      account => account.IDUser,
                      (user, account) => new { user, account })
                .Where(u => u.user.UserType == "Khách Hàng" || u.user.UserType == "Nhân Viên")
                .Where(u => u.account.VerificationCodeExpiration >= sixMonthsAgo) // Lọc theo VerificationCodeExpiration thay vì birth
                .GroupBy(u => new { u.user.UserType, u.account.VerificationCodeExpiration.Value.Year, u.account.VerificationCodeExpiration.Value.Month })
                .Select(group => new UserStatisticsByMonth
                {
                    UserType = group.Key.UserType,
                    Month = group.Key.Month,
                    Year = group.Key.Year,
                    Count = group.Count()
                })
                .OrderBy(data => data.Year).ThenBy(data => data.Month)
                .ToList();
        }

        // Tạo biểu đồ từ dữ liệu thống kê
        public static string GenerateUserStatisticsChart(List<UserStatistics> statistics)
        {
            // Tạo đối tượng Chart
            Chart chart = new Chart
            {
                Width = 600,
                Height = 400
            };

            ChartArea chartArea = new ChartArea("UserStatisticsArea");
            chart.ChartAreas.Add(chartArea);

            // Tạo Series
            Series series = new Series("Người Dùng")
            {
                ChartType = SeriesChartType.Column
            };

            foreach (var stat in statistics)
            {
                series.Points.AddXY(stat.UserType, stat.Count);
            }

            chart.Series.Add(series);

            // Tùy chỉnh giao diện biểu đồ
            chartArea.AxisX.Title = "Loại Người Dùng";
            chartArea.AxisY.Title = "Số Lượng";
            chartArea.AxisX.Interval = 1;
            chart.Titles.Add("Thống Kê Người Dùng Tháng Này");

            // Đảm bảo trục Y chỉ hiển thị số nguyên
            chartArea.AxisY.LabelStyle.Format = "{0:0}";  // Chỉ hiển thị số nguyên

            // Đường dẫn lưu biểu đồ
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string resourceFolder = Path.Combine(baseDirectory, "Resource", "img");

            if (!Directory.Exists(resourceFolder))
            {
                Directory.CreateDirectory(resourceFolder);
            }

            string filePath = Path.Combine(resourceFolder, "UserStatisticsChart.png");
            chart.SaveImage(filePath, ChartImageFormat.Png);

            return filePath;
        }



        // Thực hiện toàn bộ xử lý: lấy dữ liệu, tạo biểu đồ, gửi email
        public static void GenerateAndSendUserStatisticsChart(string recipientEmail)
        {
            List<UserStatistics> statistics = GetUserStatisticsForCurrentMonth();

            if (statistics == null || statistics.Count == 0)
            {
                Console.WriteLine("Không có dữ liệu để tạo biểu đồ.");
                return;
            }

            // Tạo biểu đồ
            string chartPath = GenerateUserStatisticsChart(statistics);

            // Gửi email
            Ctrl_Account.SendEmailWithAttachment(recipientEmail, chartPath);
            MessageBox.Show($"Biểu đồ thống kê người dùng đã được gửi tới {recipientEmail}");
        }

    }
}
