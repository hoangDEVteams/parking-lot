using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test.Controller;
using Test.Views;

namespace Test.MomoPayment
{
    public class MomoCallbackListener
    {
        public event Action<MomoCallbackData> OnCallbackReceived;
        private static string username2;
        public static void GetUsername(string username)
        {
            username2 = username;
        }
        public async Task StartListener()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://*:5000/callback/"); 
            listener.Start();
            while (true)
            {
                var context = await listener.GetContextAsync();
                var request = context.Request;

                if (request.HttpMethod == "POST")
                {
                    using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        string body = await reader.ReadToEndAsync();
                        var callbackData = System.Text.Json.JsonSerializer.Deserialize<MomoCallbackData>(body);
                        string rawSignature = $"amount={callbackData.Amount}&extraData={callbackData.ExtraData}&message={callbackData.Message}&orderId={callbackData.OrderId}&orderInfo={callbackData.OrderInfo}&orderType={callbackData.OrderType}&partnerCode={callbackData.PartnerCode}&payType={callbackData.PayType}&requestId={callbackData.RequestId}&responseTime={callbackData.ResponseTime}&resultCode={callbackData.ResultCode}&transId={callbackData.TransId}";
                        if (callbackData.ResultCode == 0)
                        {
                            MessageBox.Show("Transaction successful.");
                            ProcessRawBodyAndUpdateBalance(body);         
                            decimal balance = Ctrl_Wallet.LoadMoney(username2);
                            FMain.Call(balance);
                        }
                        else
                        {
                            MessageBox.Show($"Transaction failed. ResultCode: {callbackData.ResultCode}");
                        }

                    }
                }

                context.Response.StatusCode = 204; 
                context.Response.Close();
            }
        }

        //private bool VerifySignature(string rawData, string signature, string secretKey)
        //{
        //    using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey)))
        //    {
        //        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(rawData));
        //        var computedSignature = BitConverter.ToString(computedHash).Replace("-", "").ToLower();
        //        return computedSignature == signature;
        //    }
        //}
        private void ProcessRawBodyAndUpdateBalance(string rawBody)
        {
            try
            {
                using (var jsonDocument = System.Text.Json.JsonDocument.Parse(rawBody))
                {
                    var root = jsonDocument.RootElement;

                    int amount = root.GetProperty("amount").GetInt32();
                    int orderInfo = int.Parse(root.GetProperty("orderInfo").GetString());
                    Ctrl_Wallet.LoadMoney(username2);
                    UpdateWalletBalance(amount, orderInfo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing raw body: {ex.Message}");
            }
        }

        private void UpdateWalletBalance(int amount, int orderInfo)
        {
            try
            {
                var wallet = CUltils.db.Wallets.SingleOrDefault(w => w.IDAcc == orderInfo);

                if (wallet != null)
                {
                    wallet.Money += amount;
                    CUltils.db.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Wallet not found. Update failed.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating wallet balance: {ex.Message}");
            }
        }

        public class MomoCallbackData
        {
            public string PartnerCode { get; set; }
            public string OrderId { get; set; }
            public string RequestId { get; set; }
            public int Amount { get; set; }
            public string OrderInfo { get; set; }
            public string OrderType { get; set; }
            public long TransId { get; set; }
            public int ResultCode { get; set; }
            public string Message { get; set; }
            public string PayType { get; set; }
            public long ResponseTime { get; set; }
            public string ExtraData { get; set; }
            public string Signature { get; set; }
        }
    }
}

