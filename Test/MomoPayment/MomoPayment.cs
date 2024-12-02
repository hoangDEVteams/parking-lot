using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Test.MomoPayment
{
    public class MomoPayment
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public static async Task<string> CreatePaymentRequest(decimal amount)
        {
            try
            {
                string orderId = MomoConfig.PartnerCode + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                string requestId = orderId;

                // Raw signature format
                string rawSignature = $"accessKey={MomoConfig.AccessKey}&amount={amount}&extraData=&ipnUrl={MomoConfig.IpnUrl}" +
                                      $"&orderId={orderId}&orderInfo=pay with MoMo&partnerCode={MomoConfig.PartnerCode}" +
                                      $"&redirectUrl={MomoConfig.RedirectUrl}&requestId={requestId}&requestType={MomoConfig.RequestType}";

                // Generate HMAC SHA256 signature
                string signature = GenerateSignature(rawSignature, MomoConfig.SecretKey);

                // Request body
                var requestBody = new
                {
                    partnerCode = MomoConfig.PartnerCode,
                    partnerName = "Test",
                    storeId = "MomoTestStore",
                    requestId = requestId,
                    amount = amount,
                    orderId = orderId,
                    orderInfo = "pay with MoMo",
                    redirectUrl = MomoConfig.RedirectUrl,
                    ipnUrl = MomoConfig.IpnUrl,
                    lang = MomoConfig.Lang,
                    requestType = MomoConfig.RequestType,
                    autoCapture = true,
                    extraData = "",
                    signature = signature
                };

                string jsonBody = JsonConvert.SerializeObject(requestBody);

                // Send HTTP POST request
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("https://test-payment.momo.vn/v2/gateway/api/create", content);

                response.EnsureSuccessStatusCode();
                string responseContent = await response.Content.ReadAsStringAsync();

                return responseContent;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        private static string GenerateSignature(string rawData, string secretKey)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey)))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}
