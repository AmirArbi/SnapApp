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
    class MonthService
    {
        public static ObservableCollection<Month> Months;
        private const string URL = "http://localhost/blog/public/api/month";
        private const string URI = "https://localhost/blog/public/api/month/DeleteMonth/";
        static HttpClient _client = new HttpClient();
        public static async Task OnAdd(string GroupName, string Slug, string ProfName, string MonthName, long NumberSessions, string SchoolYear, float WantedMoney)
        {
            Month account = new Month
            {
                Slug = Slug,
                GroupName = GroupName,
                ProfName = ProfName,
                MonthName = MonthName,
                NumberSessions = NumberSessions,
                SchoolYear = SchoolYear,
                WantedMoney = WantedMoney
            };
            var content = JsonConvert.SerializeObject(account);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URL, sContent);
        }

        public static async Task<ObservableCollection<Month>> GetItemsAsync(bool forceRefresh = false)
        {
            var result = await _client.GetStringAsync(URL);
            Months = JsonConvert.DeserializeObject<ObservableCollection<Month>>(result);
            return Months;
        }
        public static async Task<ObservableCollection<Month>> GetStudentAsync(string Profname, string SchoolYear)
        {
            var result = await _client.GetStringAsync(URL + "/" + Profname  + "/" + SchoolYear);
            Months = JsonConvert.DeserializeObject<ObservableCollection<Month>>(result);
            return Months;
        }
        public static async Task<ObservableCollection<Month>> GetStudentAsync(string Profname, string GroupName, string SchoolYear)
        {
            var result = await _client.GetStringAsync(URL + "/" + Profname + "/" + GroupName + "/" + SchoolYear);
            Months = JsonConvert.DeserializeObject<ObservableCollection<Month>>(result);
            return Months;
        }
        public static async Task<ObservableCollection<Month>> GetStudentAsync(string Profname, string GroupName, string Slug, string SchoolYear)
        {
            var result = await _client.GetStringAsync(URL + "/" + Profname + "/" + GroupName + "/" + Slug + "/" + SchoolYear);
            Months = JsonConvert.DeserializeObject<ObservableCollection<Month>>(result);
            return Months;
        }
        public static async Task<ObservableCollection<Month>> GetDetailAsync(string Profname, string StudentName, string GroupName, string SchoolYear)
        {
            var result = await _client.GetStringAsync(URL + "/detail/" + Profname + "/" + StudentName + "/" + GroupName + "/" + SchoolYear);
            Months = JsonConvert.DeserializeObject<ObservableCollection<Month>>(result);
            return Months;
        }

        public static async Task UpdateCurrentSession(Month month)
        {
            month.CurrentSessions++;
            var content = JsonConvert.SerializeObject(month);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URL + "/UpdateMonth/" + month.ProfName + "/" + month.GroupName + "/" + month.Slug + "/" + month.SchoolYear, sContent);
        }
        public static async Task UpdateMonthSession(Month month)
        {
            var content = JsonConvert.SerializeObject(month);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URL + "/UpdateMonth/" + month.ProfName + "/" + month.GroupName + "/" + month.Slug + "/" + month.SchoolYear, sContent);
        }
        public async static Task<bool> DeleteItemAsync(string ProfName, string GroupName, string student_id)
        {
            await _client.GetStringAsync(URI + ProfName + '/' + GroupName + '/' + student_id);
            return await Task.FromResult(true);
        }

    }
}
