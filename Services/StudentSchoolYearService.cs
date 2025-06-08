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
    class StudentSchoolYearService
    {
        public static ObservableCollection<StudentSchoolYear> StudentSchoolYears;
        private const string URL = "http://localhost/blog/public/api/school-year";
        private const string URI = "http://localhost/blog/public/api/school-year/DeleteSchoolYear/";
        private const string URU = "http://localhost/blog/public/api/school-year/UpdateSchoolYear/";
        static HttpClient _client = new HttpClient();
        public async static Task OnAdd(string SchoolYear, string StudentName, string Level, string Section)
        {
            StudentSchoolYear account = new StudentSchoolYear
            {
                SchoolYear = SchoolYear,
                StudentName = StudentName,
                Level = Level,
                Section = Section,
            };
            var content = JsonConvert.SerializeObject(account);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URL, sContent);
        }

        public static async Task<ObservableCollection<StudentSchoolYear>> GetItemsAsync(bool forceRefresh = false)
        {
            var result = await _client.GetStringAsync(URL);
            StudentSchoolYears = JsonConvert.DeserializeObject<ObservableCollection<StudentSchoolYear>>(result);
            return StudentSchoolYears;
        }
        public static async Task<ObservableCollection<StudentSchoolYear>> GetBySchoolYearAsync(string SchoolYear)
        {
            var result = await _client.GetStringAsync(URL +  "Y/" + SchoolYear);
            StudentSchoolYears = JsonConvert.DeserializeObject<ObservableCollection<StudentSchoolYear>>(result);
            return StudentSchoolYears;
        }
        public static async Task<ObservableCollection<StudentSchoolYear>> GetByStudentNameAsync(string StudentName)
        {
            var result = await _client.GetStringAsync(URL + "S/" + StudentName);
            StudentSchoolYears = JsonConvert.DeserializeObject<ObservableCollection<StudentSchoolYear>>(result);
            return StudentSchoolYears;
        }
        public async Task<bool> AddItemAsync(StudentSchoolYear item)
        {
            StudentSchoolYears.Add(item);

            return await Task.FromResult(true);
        }

        public async static Task UpdateNumberOfStudents(StudentSchoolYear item)
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
