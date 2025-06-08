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
    class PresenceService
    {
        public static ObservableCollection<Presence> GroupStudents;
        private const string URL = "http://localhost/blog/public/api/presence";
        static HttpClient _client = new HttpClient();
        public static async Task OnAdd(string StudentName, string ProfName, string GroupName, string SchoolYear, string MonthName, long session_id, bool IsPresent)
        {
            Presence account = new Presence
            {
                StudentName = StudentName,
                ProfName = ProfName,
                SchoolYear = SchoolYear,
                MonthName = MonthName,
                GroupName = GroupName,
                session_id = session_id,
                IsPresent = IsPresent
            };

            var content = JsonConvert.SerializeObject(account);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URL, sContent);
        }

        public static async Task UpadtePresence(string StudentName, string ProfName,string GroupName, string SchoolYear, string MonthName, long session_id, bool IsPresent)
        {
            Presence account = new Presence
            {
                StudentName = StudentName,
                ProfName = ProfName,
                GroupName = GroupName,
                SchoolYear = SchoolYear,
                MonthName = MonthName,
                session_id = session_id,
                IsPresent = IsPresent
            };

            var content = JsonConvert.SerializeObject(account);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URL + "/UpdatePresence" + "/" + ProfName + "/" + StudentName + "/" + GroupName + "/" + SchoolYear + "/" + MonthName + "/" + session_id, sContent);
        }

        public static async Task<ObservableCollection<Presence>> GetItemsAsync(bool forceRefresh = false)
        {
            var result = await _client.GetStringAsync(URL);
            GroupStudents = JsonConvert.DeserializeObject<ObservableCollection<Presence>>(result);
            return GroupStudents;
        }
        public static async Task<ObservableCollection<Presence>> GetPresenceAsync(string StudentName, string ProfName, string GroupName, string SchoolYear, string MonthName, long session_id)
        {
            var result = await _client.GetStringAsync(URL + "/" + ProfName + "/" + StudentName + "/" + GroupName + "/" + SchoolYear + "/" + MonthName + "/" + session_id);
            GroupStudents = JsonConvert.DeserializeObject<ObservableCollection<Presence>>(result);
            return GroupStudents;
        }
        public static async Task<ObservableCollection<Presence>> GetAllPresenceOfStudentAsync(string StudentName, string ProfName, string SchoolYear)
        {
            var result = await _client.GetStringAsync(URL + "/" + ProfName + "/" + StudentName + "/" + SchoolYear);
            GroupStudents = JsonConvert.DeserializeObject<ObservableCollection<Presence>>(result);
            return GroupStudents;
        }
    }
}
