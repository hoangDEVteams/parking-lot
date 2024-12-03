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

        public static async Task<string> CreatePaymentRequest(decimal amount, int IDAcc)
        {
            try
            {
                string orderId = MomoConfig.PartnerCode + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                string requestId = orderId;

                string rawSignature = $"accessKey={MomoConfig.AccessKey}&amount={amount}&extraData=&ipnUrl={MomoConfig.IpnUrl}" +
                                      $"&orderId={orderId}&orderInfo={IDAcc}&partnerCode={MomoConfig.PartnerCode}" +
                                      $"&redirectUrl={MomoConfig.RedirectUrl}&requestId={requestId}&requestType={MomoConfig.RequestType}";

                string signature = GenerateSignature(rawSignature, MomoConfig.SecretKey);

                var requestBody = new
                {
                    partnerCode = MomoConfig.PartnerCode,
                    partnerName = "Test",
                    storeId = "MomoTestStore",
                    requestId = requestId,
                    amount = amount,
                    orderId = orderId,
                    orderInfo = IDAcc,
                    redirectUrl = MomoConfig.RedirectUrl,
                    ipnUrl = MomoConfig.IpnUrl,
                    lang = MomoConfig.Lang,
                    requestType = MomoConfig.RequestType,
                    autoCapture = true,
                    extraData = "",
                    signature = signature
                };

                string jsonBody = JsonConvert.SerializeObject(requestBody);

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
