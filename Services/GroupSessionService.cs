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
    class GroupSessionService
    {
        public static ObservableCollection<GroupSession> GroupSessions;
        private const string URL = "http://localhost/blog/public/api/group-session";
        private const string URI = "http://localhost/blog/public/api/group-session/DeleteGroupSession/";
        static HttpClient _client = new HttpClient();
        public static async Task OnAdd(string GroupName, string ProfName, string StudentName, DateTime Date, long session_id, string MonthName, string SchoolYear)
        {
            GroupSession account = new GroupSession
            {
                StudentName = StudentName,
                ProfName = ProfName,
                Date = Date,
                session_id = session_id,
                MonthName = MonthName,
                SchoolYear = SchoolYear,
                GroupName = GroupName,
            };
            var content = JsonConvert.SerializeObject(account);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URL, sContent);
        }
        public static async Task UpdateAll(string ProfName, string GroupName, string SchoolYear, string MonthName, long session_id)
        {
            var content = JsonConvert.SerializeObject("1");
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URL + "/AllPresent/" + ProfName + "/" + GroupName + "/" + SchoolYear + "/" + MonthName + "/" + session_id, sContent);
        }
        public static async Task UpdateSession(GroupSession session)
        {
            
            var content = JsonConvert.SerializeObject(session);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URL + "/UpdateGroupSession", sContent);
        }
        public static async Task<ObservableCollection<GroupSession>> GetItemsAsync(bool forceRefresh = false)
        {
            var result = await _client.GetStringAsync(URL);
            GroupSessions = JsonConvert.DeserializeObject<ObservableCollection<GroupSession>>(result);
            return GroupSessions;
        }
        public static async Task<ObservableCollection<GroupSession>> GetSessionAsync(string ProfGroup, string GroupName, string Month)
        {
            var result = await _client.GetStringAsync(URL + "/" + ProfGroup + "/" + GroupName + "/" + Month);
            GroupSessions = JsonConvert.DeserializeObject<ObservableCollection<GroupSession>>(result);
            return GroupSessions;
        }
        public static async Task<ObservableCollection<GroupSession>> GetStudentSession(string StudentName, string ProfName, string GroupName, string MonthName)
        {
            var result = await _client.GetStringAsync(URL + "/" + StudentName + "/" + ProfName + "/" + GroupName + "/" + MonthName);
            GroupSessions = JsonConvert.DeserializeObject<ObservableCollection<GroupSession>>(result);
            return GroupSessions;
        }
        public static async Task<ObservableCollection<GroupSession>> LoadPresence(string Profname, string GroupName, string SchoolYear, string MonthName, long session_id)
        {
            var result = await _client.GetStringAsync(URL + "/" + Profname + "/" + GroupName + "/" + SchoolYear + "/" + MonthName + "/" + session_id);
            GroupSessions = JsonConvert.DeserializeObject<ObservableCollection<GroupSession>>(result);
            return GroupSessions;
        }
        public static async Task<ObservableCollection<GroupSession>> GetSession(string Profname, string GroupName, string StudentName, string SchoolYear, string MonthName, long session_id)
        {
            var result = await _client.GetStringAsync(URL + "/" + Profname + "/" + GroupName + "/" + StudentName + "/" + SchoolYear + "/" + MonthName + "/" + session_id);
            GroupSessions = JsonConvert.DeserializeObject<ObservableCollection<GroupSession>>(result);
            return GroupSessions;
        }
        public static async Task<ObservableCollection<GroupSession>> GetStudentSession(string StudentName, string ProfName)
        {
            var result = await _client.GetStringAsync(URL + "/" + StudentName + "/" + ProfName );
            GroupSessions = JsonConvert.DeserializeObject<ObservableCollection<GroupSession>>(result);
            return GroupSessions;
        }
        public async Task<bool> AddItemAsync(GroupSession item)
        {
            GroupSessions.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(GroupSession item)
        {
            var oldItem = GroupSessions.Where((GroupSession arg) => arg.GroupName == item.GroupName).FirstOrDefault();
            GroupSessions.Remove(oldItem);
            GroupSessions.Add(item);
            return await Task.FromResult(true);
        }
        public async static Task<bool> DeleteItemAsync(string GroupName, string ProfName, string session_id, string MonthName)
        {
            await _client.GetStringAsync(URI + ProfName + '/' + GroupName + '/' + session_id + '/' + MonthName);
            return await Task.FromResult(true);
        }

    }
}

