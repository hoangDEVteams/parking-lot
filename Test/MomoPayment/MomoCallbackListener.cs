using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Test.MomoPayment
{
    internal class MomoCallbackListener
    {
        public async Task StartListener()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://+:5000/callback/");
            listener.Start();

            Console.WriteLine("Listening for MoMo callback...");

            while (true)
            {
                var context = await listener.GetContextAsync();
                var request = context.Request;

                if (request.HttpMethod == "POST")
                {
                    using (var reader = new System.IO.StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        string body = await reader.ReadToEndAsync();
                        Console.WriteLine($"Callback received: {body}");

                        // TODO: Process callback (e.g., update order status in database)
                    }
                }

                context.Response.StatusCode = 204; // No Content
                context.Response.Close();
            }
        }
    }
}
