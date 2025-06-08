using Newtonsoft.Json;
using SDKTemplate.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate.Services
{
    class PasswordService
    {
        private const string URL = "http://localhost/blog/public/api/password";
        static HttpClient _client = new HttpClient();
        public static async Task OnAdd(string AdminName, string AdminPassword)
        {
            Password account = new Password
            {
                AdminName = AdminName,
                AdminPassword = AdminPassword,
            };
            var content = JsonConvert.SerializeObject(account);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URL, sContent);
        }
        public static async Task<string> CheckPassword(Password password)
        {
            var result = await _client.GetStringAsync(URL + "/Check/" + password.AdminName + "/" + password.AdminPassword);
            var res = JsonConvert.DeserializeObject<string>(result);
            return res;
        }

        public static async Task UpdatePasswordAsync(Password password)
        {
            var content = JsonConvert.SerializeObject(password);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URL + "/UpdatePassword/" + password.AdminName, sContent);
        }
    }
}
