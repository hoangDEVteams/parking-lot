using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace Test.ZaloPay
{
    public class CZLPayAPI
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<string> CreateOrder()
        {
            string endpoint = "https://sb-openapi.zalopay.vn/v2/create";
            string appId = "2553";
            string key1 = "PcY4iZIKFCIdgZvA6ueMcMHHUbRLYjPL";

            var transID = new Random().Next(1000000);
            var order = new
            {
                app_id = appId,
                app_trans_id = $"{DateTime.Now:yyMMdd}_{transID}",
                app_user = "user123",
                app_time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                amount = 50000,
                embed_data = "{}",
                item = "[]",
                description = $"Payment for order #{transID}",
                callback_url = "https://b074-1-53-37-194.ngrok-free.app/callback",
                bank_code = "zalopayapp"
            };

            string data = $"{appId}|{order.app_trans_id}|{order.app_user}|{order.amount}|{order.app_time}|{order.embed_data}|{order.item}";
            string mac = HMac256CPT.ComputeHMACSHA256(data, key1);

            var payload = new
            {
                order.app_id,
                order.app_trans_id,
                order.app_user,
                order.app_time,
                order.amount,
                order.embed_data,
                order.item,
                order.description
            };

            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                Console.WriteLine("Sending request to: " + endpoint);
                Console.WriteLine("Payload: " + json);

                var response = await client.PostAsync(endpoint, content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response: " + responseBody);

                return responseBody;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return $"Error: {e.Message}";
            }
        }
    }
}
