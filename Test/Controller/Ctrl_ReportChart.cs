using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System;
using Test;
using System.Linq;  


public class ReportChart
{ 
    public void GenerateColumnChart(Chart chart, DateTime startDate, DateTime endDate, string reportType)
    {
        chart.Series.Clear();  
         
        var series = new Series("Report")
        {
            ChartType = SeriesChartType.Column, 
            IsValueShownAsLabel = true, 
            BorderWidth = 2  
        };
         
        series.Points.Clear();  
         
        var data = GetDataForReport(startDate, endDate, reportType);
         
        foreach (var item in data)
        {
            series.Points.AddXY(item.Date, item.Value);  
        }
         
        chart.Series.Add(series);
         
        chart.ChartAreas[0].AxisX.Title = "Date";
        chart.ChartAreas[0].AxisY.Title = "Value";
    }

    public static void SaveChartAsImage(Chart chart, string filePath)
    {
        try
        {
            // Lưu biểu đồ dưới dạng file ảnh PNG
            chart.SaveImage(filePath, ChartImageFormat.Png);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi khi lưu biểu đồ: {ex.Message}");
        }
    }

    private List<ReportData> GetDataForReport(DateTime startDate, DateTime endDate, string reportType)
    {
        var data = new List<ReportData>();

        using (var db = new BTXEntities1())  
        {
            if (reportType == "Customer Rentals")
            {
                data = (from rd in db.RentalDetails
                        join r in db.Rentals on rd.IDRental equals r.IDRental
                        where r.RentalDate >= startDate && r.RentalDate <= endDate
                        group rd by r.RentalDate into g
                        select new ReportData
                        {
                            Date = g.Key,
                            Value = g.Count() 
                        })
                        .ToList();
            }
            else if (reportType == "Penalty Amounts")
            {
                data = db.Penalties
                    .Where(p => p.PenaltyDate >= startDate && p.PenaltyDate <= endDate)
                    .GroupBy(p => p.PenaltyDate)
                    .Select(g => new ReportData
                    {
                        Date = g.Key,
                        Value = g.Sum(p => p.PenaltyDetails.Sum(pd => pd.price)) // Tổng tiền phạt
                    })
                    .ToList();
            }
            else if (reportType == "Total Earnings")
            {
                data = (from rd in db.RentalDetails
                        join r in db.Rentals on rd.IDRental equals r.IDRental
                        where r.RentalDate >= startDate && r.RentalDate <= endDate
                        group rd by r.RentalDate into g
                        select new ReportData
                        {
                            Date = g.Key,
                            Value = g.Sum(rd => rd.RentPrice) // Tổng thu nhập từ thuê xe
                        })
                        .ToList();
            }
            else if (reportType == "Employee Salaries")
            {
                data = db.Employees
                    .Where(e => e.DateHired >= startDate && e.DateHired <= endDate)
                    .GroupBy(e => e.DateHired)
                    .Select(g => new ReportData
                    {
                        Date = g.Key,
                        Value = (decimal)g.Sum(e => e.salary) 
                    })
                    .ToList();
            }
        }

        return data;
    }

}

// Lớp dùng để chứa dữ liệu báo cáo (ngày và giá trị)
public class ReportData
{
    public DateTime Date { get; set; }
    public decimal Value { get; set; }
}
