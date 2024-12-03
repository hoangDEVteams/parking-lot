using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Test.Controller
{
    internal class Ctrl_Account
    {
        public static string RegisterAccount(string username, string password, string email, string salt)
        {
            var existingUser = CUltils.db.Accounts.SingleOrDefault(a => a.Username == username);
            if (existingUser != null)
            {
                return "Tên đăng nhập đã tồn tại.";
            }

            var newAccount = new Account
            {
                Username = username,
                Password = password, 
                Email = email,
                Role = "KH", 
                Status = "InActive", 
                Salt = salt,
            };

            CUltils.db.Accounts.Add(newAccount);

            try
            {
                CUltils.db.SaveChanges();

                string verificationCode = GenerateVerificationCode();
                CreateWalletForAccount(newAccount.IDAcc);
                return "Đăng ký thành công!";
            }
            catch (Exception ex)
            {
                return $"Lỗi khi lưu vào cơ sở dữ liệu: {ex.Message}";
            }
        }
        private static void CreateWalletForAccount(int accountId)
        {
            var wallet = new Wallet
            {
                IDAcc = accountId,
                Money = 0,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            CUltils.db.Wallets.Add(wallet);
            CUltils.db.SaveChanges();
        }

        public static void SaveVerificationCodeToDatabase(string username, string verificationCode)
        {
            var account = CUltils.db.Accounts.SingleOrDefault(a => a.Username == username);
            if (account != null)
            {
                account.VerificationCode = verificationCode;
                account.VerificationCodeExpiration = DateTime.Now.AddMinutes(10); 
                CUltils.db.SaveChanges();
            }
        }

        public static string GenerateVerificationCode(int length = 6)
        {
            Random random = new Random();
            string verificationCode = string.Empty;
            for (int i = 0; i < length; i++)
            {
                verificationCode += random.Next(0, 10).ToString(); 
            }
            return verificationCode;
        }
        public static string UpdateAccountStatusToActive(string username)
        {
            try
            {
                var account = CUltils.db.Accounts.SingleOrDefault(a => a.Username == username);

                if (account != null)
                {
                    account.Status = "Active";
                    CUltils.db.SaveChanges();
                    return "Cập nhật trạng thái thành công!";
                }
                else
                {
                    return "Tài khoản không tồn tại.";
                }
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }
        public static void SendVerificationEmail(string recipientEmail, string verificationCode)
        {
            string senderEmail = "hhbakery5@gmail.com"; 
            string senderPassword = "vscw ldrh vdfk xgml"; 

            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = true,
                };

                MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail)
                {
                    Subject = "Mã xác nhận tài khoản",
                    Body = $"Mã xác nhận của bạn là: {verificationCode}, \nLưu ý: Mã chỉ có hạn trong 10 phút !"
                };

                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi gửi email: {ex.Message}");
            }
        }
        public static string CreateUser(string username)
        {
            try
            {
                // Lấy thông tin tài khoản từ username
                var account = CUltils.db.Accounts.SingleOrDefault(a => a.Username == username);
                if (account == null)
                {
                    return "Tài khoản không tồn tại.";
                }

                // Tìm UserID lớn nhất
                var maxUserId = CUltils.db.Users
                    .OrderByDescending(u => u.IDUser)
                    .Select(u => u.IDUser)
                    .FirstOrDefault();

                string newUserId = GenerateNewId(maxUserId, "U");

                // Tạo User mới
                var newUser = new User
                {
                    IDUser = newUserId,
                    Name = "test",
                    Gender = "test",
                    PhoneNumber = "123456789",
                    Address = "POLOLOPO",
                    IdentityCard = "123456789",
                    BankNumber = "123456789",
                    UserType = "KH",
                    IDAcc = account.IDAcc,
                    birth = DateTime.Now
                };

                CUltils.db.Users.Add(newUser);
                CUltils.db.SaveChanges();
                account.IDUser = newUserId;

                return "Tạo User thành công!";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi chi tiết: {ex.InnerException?.Message ?? ex.Message}");
                return $"Lỗi khi tạo User: {ex.Message}";
            }
        }
        public static int GetIDAcc(string username)
        {
            var account = CUltils.db.Accounts.SingleOrDefault(a => a.Username == username);
            if (account != null)
            {
                return account.IDAcc;
            }
            return 0;
        }
        public static int GetIDAccbyUserID(string IDUser)
        {
            var account = CUltils.db.Accounts.SingleOrDefault(a => a.IDUser == IDUser);
            if (account != null)
            {
                return account.IDAcc;
            }
            return 0;
        }
        public static string GetUserID(string username)
        {
            var account = CUltils.db.Accounts.SingleOrDefault(a => a.Username == username);
            if (account != null)
            {
                return account.IDUser;
            }
            return null;
        }
        public static string CreateCustomer(string userId)
        {
            try
            {
                var user = CUltils.db.Users.SingleOrDefault(u => u.IDUser == userId);
                if (user == null)
                {
                    return "User không tồn tại.";
                }
                var maxCustomerId = CUltils.db.Customers
                    .OrderByDescending(c => c.IDCustomer)
                    .Select(c => c.IDCustomer)
                    .FirstOrDefault();

                string newCustomerId = GenerateNewId(maxCustomerId, "KH");

                var newCustomer = new Customer
                {
                    IDCustomer = newCustomerId,
                    IDUser = userId, 
                    MembershipLevel = "Normal",
                    Points = 0
                };

                CUltils.db.Customers.Add(newCustomer);
                CUltils.db.SaveChanges();

                return "Tạo Customer thành công!";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi chi tiết: {ex.InnerException?.Message ?? ex.Message}");
                return $"Lỗi khi tạo Customer: {ex.Message}";
            }
        }
        private static string GenerateNewId(string currentId, string prefix)
        {
            if (string.IsNullOrEmpty(currentId))
            {
                return $"{prefix}001"; 
            }

            int numericPart = int.Parse(currentId.Substring(prefix.Length));
            numericPart++;

            return $"{prefix}{numericPart:D3}";
        }

    }
}
