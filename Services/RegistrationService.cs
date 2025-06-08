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
    class RegistrationService
    {
        public static ObservableCollection<Registration> Registrations;
        private const string URL = "http://localhost/blog/public/api/registration";
        private const string URI = "https://localhost/blog/public/api/registration/DeleteRegistration/";
        static HttpClient _client = new HttpClient();
        async void OnAdd(object sender, System.EventArgs e)
        {
            Registration account = new Registration
            {
                ProfName = "Title " + DateTime.Now.Ticks

            };
            var content = JsonConvert.SerializeObject(account);
            await _client.PostAsync(URL, new StringContent(content));
        }

        public static async Task<ObservableCollection<Registration>> GetItemsAsync(bool forceRefresh = false)
        {
            var result = await _client.GetStringAsync(URL);
            Registrations = JsonConvert.DeserializeObject<ObservableCollection<Registration>>(result);
            return Registrations;
        }
        public async Task<bool> AddItemAsync(Registration item)
        {
            Registrations.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Registration item)
        {
            var oldItem = Registrations.Where((Registration arg) => arg.ProfName == item.ProfName).FirstOrDefault();
            Registrations.Remove(oldItem);
            Registrations.Add(item);
            return await Task.FromResult(true);
        }
        public async static Task<bool> DeleteItemAsync(string ProfName, string StudentName)
        {
            await _client.GetStringAsync(URI + ProfName + '/' + StudentName);
            return await Task.FromResult(true);
        }

        public async Task<Registration> GetItemAsync(string ProfName, string StudentName)
        {
            return await Task.FromResult(Registrations.FirstOrDefault(s => s.ProfName == ProfName));
        }
    }
}
