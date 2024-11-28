using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
                Salt = salt 
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

    }
}
