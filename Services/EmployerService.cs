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
    class EmployerService
    {
        public static ObservableCollection<Employer> GroupStudents;
        private const string URL = "http://localhost/blog/public/api/employer";
        static HttpClient _client = new HttpClient();
        public static async Task OnAdd(string EmployerName, string Name, string Surname, string Role)
        {
            Employer account = new Employer
            {
                EmployerName = EmployerName,
                Name = Name,
                Surname = Surname,
                Role = Role,
            };
            var content = JsonConvert.SerializeObject(account);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URL, sContent);
        }
        public static async Task Update(Employer employer, string EmployerName)
        {
            var content = JsonConvert.SerializeObject(employer);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URL + "/UpdateEmployer/" + EmployerName, sContent);
        }
        public static async Task<ObservableCollection<Employer>> GetItemsAsync(bool forceRefresh = false)
        {
            var result = await _client.GetStringAsync(URL);
            GroupStudents = JsonConvert.DeserializeObject<ObservableCollection<Employer>>(result);
            return GroupStudents;
        }
    }
}
