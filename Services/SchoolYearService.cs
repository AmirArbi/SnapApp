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
    class SchoolYearService
    {
        public static ObservableCollection<Schoolyear> SchoolYears;
        private const string URL = "http://localhost/blog/public/api/school-year";
        private const string URI = "http://localhost/blog/public/api/school-year/DeleteSchoolYear/";
        private const string URU = "http://localhost/blog/public/api/school-year/UpdateSchoolYear/";
        static HttpClient _client = new HttpClient();
        public async static Task OnAdd(string SchoolYear, DateTime Begining, DateTime Ending)
        {
            Schoolyear account = new Schoolyear
            {
                Begining = Begining,
                Ending = Ending,
                SchoolYear = SchoolYear,
            };
            var content = JsonConvert.SerializeObject(account);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URL, sContent);
        }

        public static async Task<ObservableCollection<Schoolyear>> GetItemsAsync(bool forceRefresh = false)
        {
            var result = await _client.GetStringAsync(URL);
            SchoolYears = JsonConvert.DeserializeObject<ObservableCollection<Schoolyear>>(result);
            return SchoolYears;
        }
        public static async Task<ObservableCollection<Schoolyear>> GetSchoolYearAsync(string SchoolYear)
        {
            var result = await _client.GetStringAsync(URL + "/"  + SchoolYear);
            SchoolYears = JsonConvert.DeserializeObject<ObservableCollection<Schoolyear>>(result);
            return SchoolYears;
        }
        public async Task<bool> AddItemAsync(Schoolyear item)
        {
            SchoolYears.Add(item);

            return await Task.FromResult(true);
        }

        public async static Task UpdateNumberOfStudents(Schoolyear item)
        {
            var content = JsonConvert.SerializeObject(item);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URU + item.SchoolYear, sContent);
        }
        public async static Task<bool> DeleteItemAsync(string SchoolYear)
        {
            await _client.GetStringAsync(URI + SchoolYear);
            return await Task.FromResult(true);
        }
    }
}
