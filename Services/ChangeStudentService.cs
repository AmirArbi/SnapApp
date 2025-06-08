using Newtonsoft.Json;
using SDKTemplate.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate.Services
{
    class ChangeStudentService
    {
        public static ObservableCollection<ChangeStudent> GroupStudents;
        private const string URL = "http://localhost/blog/public/api/change-student";
        static HttpClient _client = new HttpClient();
        public static async Task OnAdd(string OldGroup, string SchoolYear, string StudentName, string ProfName, string MonthName, int CurrentSession)
        {
            ChangeStudent account = new ChangeStudent
            {
                OldGroup = OldGroup,
                StudentName = StudentName,
                ProfName = ProfName,
                MonthName = MonthName,
                SchoolYear = SchoolYear,
                CurrentSession = CurrentSession
            };
            var content = JsonConvert.SerializeObject(account);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URL, sContent);
        }

        public static async Task<ObservableCollection<ChangeStudent>> GetItemsAsync(bool forceRefresh = false)
        {
            var result = await _client.GetStringAsync(URL);
            GroupStudents = JsonConvert.DeserializeObject<ObservableCollection<ChangeStudent>>(result);
            return GroupStudents;
        }
        public static async Task<ObservableCollection<ChangeStudent>> GetChangementAsync(string StudentName, string ProfName, string OldGroup, string SchoolYear, string MonthName)
        {
            var result = await _client.GetStringAsync(URL + "/" + StudentName + "/" + ProfName + "/" + OldGroup + "/" + SchoolYear + "/" + MonthName);
            GroupStudents = JsonConvert.DeserializeObject<ObservableCollection<ChangeStudent>>(result);
            return GroupStudents;
        }
        public async Task<bool> UpdateItemAsync(ChangeStudent item)
        {
            var oldItem = GroupStudents.Where((ChangeStudent arg) => arg.ProfName == item.ProfName).FirstOrDefault();
            GroupStudents.Remove(oldItem);
            GroupStudents.Add(item);
            return await Task.FromResult(true);
        }
        public async static Task<bool> DeleteItemAsync(ChangeStudent student)
        {
            await _client.GetStringAsync(URL + "/DeleteGroupStudent/" + student.id);
            return await Task.FromResult(true);
        }
    }
}
