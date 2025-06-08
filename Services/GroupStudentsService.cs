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
    class GroupStudentsService
    {
        public static ObservableCollection<GroupStudents> GroupStudents;
        private const string URL = "http://localhost/blog/public/api/group-students";
        static HttpClient _client = new HttpClient();
         public static async Task OnAdd(string GroupName, string SchoolYear, string StudentName, string ProfName, long studentId)
        {
            GroupStudents account = new GroupStudents
            {
                GroupName = GroupName,
                StudentName = StudentName,
                ProfName = ProfName,
                student_id = studentId,
                SchoolYear = SchoolYear
            };
            var content = JsonConvert.SerializeObject(account);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URL, sContent);
        }
        public static async Task UpdateStudent(GroupStudents student, string ProfName,string SchoolYear, string GroupName, long studentId)
        {
            var content = JsonConvert.SerializeObject(student);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URL + "/UpdateGroupStudent/" + ProfName + "/" + SchoolYear + "/" + GroupName + "/" + studentId, sContent);
        }
        public static async Task<ObservableCollection<GroupStudents>> GetItemsAsync(bool forceRefresh = false)
        {
            var result = await _client.GetStringAsync(URL);
            GroupStudents = JsonConvert.DeserializeObject<ObservableCollection<GroupStudents>>(result);
            return GroupStudents;
        }

        public static async Task<ObservableCollection<GroupStudents>> GetStudentAsync(string ProfGroup, string GroupName,string SchoolYear)
        {
            var result = await _client.GetStringAsync(URL + "/group/" + ProfGroup + "/" + GroupName + "/" + SchoolYear);
            GroupStudents = JsonConvert.DeserializeObject<ObservableCollection<GroupStudents>>(result);
            return GroupStudents;
        }
        public static async Task<ObservableCollection<GroupStudents>> GetStudentGroup(string ProfGroup, string StudentName, string SchoolYear)
        {
            var result = await _client.GetStringAsync(URL + "/student/" + ProfGroup + "/" + StudentName + "/" + SchoolYear);
            GroupStudents = JsonConvert.DeserializeObject<ObservableCollection<GroupStudents>>(result);
            return GroupStudents;
        }
        public static async Task<ObservableCollection<GroupStudents>> GetStudentGroups(string StudentName, string SchoolYear)
        {
            var result = await _client.GetStringAsync(URL + "/" + StudentName + "/" + SchoolYear);
            GroupStudents = JsonConvert.DeserializeObject<ObservableCollection<GroupStudents>>(result);
            return GroupStudents;
        }
        public static async Task<ObservableCollection<GroupStudents>> GetDetail(string ProfName, string GroupName, string SchoolYear)
        {
            var result = await _client.GetStringAsync(URL + "/group-detail/" + ProfName + "/" + GroupName + "/" + SchoolYear);
            GroupStudents = JsonConvert.DeserializeObject<ObservableCollection<GroupStudents>>(result);
            return GroupStudents;
        }
        public static async Task UpdateNote(GroupStudents student, string Note)
        {
            student.Note = Note;
            var content = JsonConvert.SerializeObject(student);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URL + "/UpdateGroupStudent/" + student.ProfName +  "/" + student.SchoolYear + "/" + student.GroupName + "/" + student.student_id, sContent);
        }
        public static async Task DeleteSession(GroupSession session)
        {
            var content = JsonConvert.SerializeObject(session);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URL + "/DeleteSession", sContent);
        }
        public static async Task UpdateReduction(GroupStudents student, float reduction)
        {
            student.Reduction = reduction;
            var content = JsonConvert.SerializeObject(student);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URL + "/UpdateGroupStudent/" + student.ProfName +  "/" + student.SchoolYear + "/" + student.GroupName + "/" + student.student_id, sContent);
        }
        public async static Task<bool> DeleteItemAsync(GroupStudents student)
        {
            await _client.GetStringAsync(URL + "/DeleteGroupStudent/" + student.ProfName + '/' + student.SchoolYear + '/' + student.GroupName + '/' + student.student_id);
            return await Task.FromResult(true);
        }
    }
}
