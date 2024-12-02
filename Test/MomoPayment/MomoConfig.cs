using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.MomoPayment
{
    public static class MomoConfig
    {
        public static string AccessKey = "F8BBA842ECF85";
        public static string SecretKey = "K951B6PE1waDMi640xX08PD3vg6EkVlz";
        public static string PartnerCode = "MOMO";
        public static string RedirectUrl = "http://localhost:5000/views/home.html";
        public static string IpnUrl = "https://ce2f-2405-4803-d757-1540-5d2c-a0fc-bbbd-30ea.ngrok-free.app/callback";
        public static string RequestType = "payWithMethod";
        public static string Lang = "vi";
    }

}
