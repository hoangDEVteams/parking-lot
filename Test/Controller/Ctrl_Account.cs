using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Test.AddOn;

namespace Test.Controller
{
    internal class Ctrl_Account

        
    {
        public static List<object> GetAccounts()
        {
            var acc = CUltils.db.Accounts
                .Select(a => new
                {
                    a.IDAcc,
                    a.Username,
                    a.Email,
                    a.Role,
                    a.Status,
                    a.IDUser
                })
                .ToList();

            return acc.Cast<object>().ToList();
        }
        public static List<AddOn.CDTOAccount> GetAccountsByUS(string username)
        {
            var acc = CUltils.db.Accounts
            .Where(a => a.Username == username)
            .Select(a => new CDTOAccount
            {
                IDAcc = a.IDAcc,
                Username = a.Username,
                Email = a.Email,
                Role = a.Role,
                Status = a.Status
            })
            .ToList();

            return acc;
        }

        public static string Login(string username, string password)
        {
            var account = CUltils.db.Accounts.SingleOrDefault(a => a.Username == username);
            if (account == null)
            {
                return "Tài khoản không tồn tại.";
            }

            if (account.Status == "InActive")
            {
                return "Tài khoản chưa được kích hoạt.";
            }

            string hashedPassword = CPass.HashPasswordWithSalt(password, account.Salt);
            if (account.Password != hashedPassword)
            {
                return "Mật khẩu không chính xác.";
            }

            return "Đăng nhập thành công!";
        }
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
        public static void CreateWalletForAccount(int accountId)
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
        public static string ChangeRole(int IDAcc, string role)
        {
            try
            {
                var account = CUltils.db.Accounts.SingleOrDefault(a => a.IDAcc == IDAcc);

                if (account != null)
                {
                    account.Role = role;
                    CUltils.db.SaveChanges();
                    return "Success";
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
        public static void SendPasswordEmail(string recipientEmail, string password, string username, string name)
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
                    Subject = "Thông tin tài khoản của bạn",
                    Body = $"Chào bạn, {name} \n\nTài khoản của bạn đã được tạo thành công. Tài khoản của bạn là: [{username}]. Mật khẩu của bạn là: [{password}]. \nLưu ý: Vui lòng thay đổi mật khẩu ngay sau khi đăng nhập."
                };

                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi gửi email: {ex.Message}");
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
                var account = CUltils.db.Accounts.SingleOrDefault(a => a.Username == username);
                if (account == null)
                {
                    return "Tài khoản không tồn tại.";
                }

                var maxUserId = CUltils.db.Users
                    .OrderByDescending(u => u.IDUser)
                    .Select(u => u.IDUser)
                    .FirstOrDefault();

                string newUserId = GenerateNewId(maxUserId, "U");

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
                    return "User not exist!";
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
                    MembershipLevel = "Bronze",
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
        public string GenerateRandomPassword()
        {

            var random = new Random();
            var length = 8;
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var password = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                password.Append(validChars[random.Next(validChars.Length)]);
            }
            return password.ToString();
        }
        public string GenerateRandomVerificationCode()
        {

            Random random = new Random();
            string verificationCode = random.Next(100000, 999999).ToString();

            return verificationCode;
        }
        public int taoACCNV()
        {
            var maxID = CUltils.db.Accounts
                .Select(a => a.IDAcc)
                .OrderByDescending(id => id)
                .FirstOrDefault();

            if (maxID == 0)
            {
                return 1;
            }

            return maxID + 1;
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
