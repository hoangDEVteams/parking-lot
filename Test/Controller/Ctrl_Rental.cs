using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Test.AddOn;

namespace Test.Controller
{
    internal class Ctrl_Rental
    {
        public List<object> RentalData()
        {
            var rental = CUltils.db.Rentals.Include("RentalDetails").Select(r => new
                {
                    r.IDRental,
                    r.RentalDate,
                    LicensePlate = r.RentalDetails.Select(rd => rd.LicensePlate).FirstOrDefault(),
                    RentPrice = r.RentalDetails.Select(rd => rd.RentPrice).FirstOrDefault(),
                    RentalDays = r.RentalDetails.Select(rd => rd.RentalDays).FirstOrDefault(),
                    RentalStatus = r.Status,
                    TotalPrice = r.RentalDetails.Select(rd => rd.RentPrice).FirstOrDefault() * r.RentalDetails.Select(rd => rd.RentalDays).FirstOrDefault()


            })
                .ToList();

            return rental.Cast<object>().ToList();
        }
        public List<object> GetRentalDetailsByCustomer(string customerId)
        {
            var rentalDetails = CUltils.db.Rentals
                .Where(r => r.IDCustomer == customerId)
                .SelectMany(r => r.RentalDetails, (r, rd) => new
                {
                    r.IDRental,
                    r.RentalDate,
                    r.IDCustomer,
                    rd.LicensePlate,
                    rd.RentPrice,
                    rd.RentalDays,
                    r.Status,
                    VehicleStatus = CUltils.db.Vehicles.FirstOrDefault(v => v.LicensePlate == rd.LicensePlate).Status
                })
                .ToList();

            return rentalDetails.Cast<object>().ToList();
        }

        public CRentalResult CreateRental(string customerId, string vehiclePlate, string status, string currentEmployeeId, int rentalDays)
        {
            var result = new CRentalResult();

            try
            {
                var maxRentalId = CUltils.db.Rentals
                    .OrderByDescending(r => r.IDRental)
                    .Select(r => r.IDRental)
                    .FirstOrDefault();

                int newRentalIdNumber = maxRentalId != null ? int.Parse(maxRentalId.Substring(1)) + 1 : 1;
                string newRentalId = "R" + newRentalIdNumber.ToString("D3");

                var vehicle = CUltils.db.Vehicles.FirstOrDefault(v => v.LicensePlate == vehiclePlate);
                if (vehicle == null)
                {
                    throw new Exception("Vehicle not found.");
                }

                if (vehicle.Status != "Available")
                {
                    throw new Exception("Vehicle is not available for rental.");
                }

                decimal rentPrice = vehicle.price;

                var rental = new Rental
                {
                    IDRental = newRentalId,
                    RentalDate = DateTime.Now,
                    IDCustomer = customerId,
                    IDEmployee = currentEmployeeId,
                    Status = status
                };
                CUltils.db.Rentals.Add(rental);

                var maxRentalDetailId = CUltils.db.RentalDetails
                    .OrderByDescending(rd => rd.IDRentalDetail)
                    .Select(rd => rd.IDRentalDetail)
                    .FirstOrDefault();

                int newRentalDetailIdNumber = maxRentalDetailId != null ? int.Parse(maxRentalDetailId.Substring(2)) + 1 : 1;
                string newRentalDetailId = "RD" + newRentalDetailIdNumber.ToString("D3");

                var rentalDetail = new RentalDetail
                {
                    IDRentalDetail = newRentalDetailId,
                    IDRental = newRentalId,
                    LicensePlate = vehiclePlate,
                    RentalDays = rentalDays,
                    RentPrice = rentPrice,
                    price = 0
                };
                CUltils.db.RentalDetails.Add(rentalDetail);

                vehicle.Status = "Unavailable";

                CUltils.db.SaveChanges();

                result.RentalId = newRentalId;
                result.RentPrice = rentPrice;
                result.Success = true;
            }
            catch (Exception ex)
            {
                // Log lỗi (nếu cần)
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }



        public List<object> GetRentalDetailsByEmployee(string employeeId)
        {
            var rentalDetails = CUltils.db.Rentals
                .Include("RentalDetails")
                .Where(r => r.IDEmployee == employeeId)
                .SelectMany(r => r.RentalDetails, (r, rd) => new
                {
                    r.RentalDate,
                    r.IDEmployee,
                    rd.LicensePlate,
                    rd.RentPrice,
                    rd.RentalDays
                })
                .ToList();

            return rentalDetails.Cast<object>().ToList();
        }
        public bool UpdateRentalStatus(string rentalId, string newStatus)
        {
            try
            {
                // Tìm hóa đơn cần cập nhật dựa trên IDRental
                var rentalToUpdate = CUltils.db.Rentals.FirstOrDefault(r => r.IDRental == rentalId);
                if (rentalToUpdate == null)
                {
                    MessageBox.Show("Rental not found.");
                    return false;
                }

                // Tìm phương tiện liên quan đến hóa đơn
                var rentalDetail = CUltils.db.RentalDetails.FirstOrDefault(rd => rd.IDRental == rentalId);
                if (rentalDetail == null)
                {
                    MessageBox.Show("Rental detail not found.");
                    return false;
                }

                var vehicle = CUltils.db.Vehicles.FirstOrDefault(v => v.LicensePlate == rentalDetail.LicensePlate);
                if (vehicle == null)
                {
                    MessageBox.Show("Vehicle not found.");
                    return false;
                }

                // Cập nhật trạng thái hóa đơn
                rentalToUpdate.Status = newStatus;

                // Nếu trạng thái mới là "Complete", cập nhật phương tiện thành "Available"
                if (newStatus == "Complete")
                {
                    vehicle.Status = "Available";
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                CUltils.db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log lỗi (nếu cần)
                MessageBox.Show($"Lỗi: {ex.Message}");
                return false;
            }
        }


    }
}
