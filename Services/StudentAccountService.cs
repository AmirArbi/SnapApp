using Newtonsoft.Json;
using SDKTemplate.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace SDKTemplate.Services
{
    class StudentAccountService
    {
        public static ObservableCollection<StudentAccount> StudentAccounts;
        private const string URL = "http://localhost/blog/public/api/student-accounts";
        private const string URI = "https://localhost/blog/public/api/student-accounts/DeleteStudentAccount/";
        static HttpClient _client = new HttpClient();
        public static async Task OnAdd(string Username, string Email, string Password, string School, string Name, 
            string Surname, string FeedBack, string Avrage, string PhoneNumber, ImageSource Image, string Gender)
        {
            StudentAccount account = new StudentAccount
            {
                Username = Username,
                Email = Email,
                Password = Password,
                School = School,
                Name = Name,
                Surname = Surname,
                FeedBack = FeedBack,
                Avrage = Avrage,
                PhoneNumber = PhoneNumber,
                //Image = Image,
                Gender = Gender,
            };
            var content = JsonConvert.SerializeObject(account);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URL, sContent);
        }

        public static async Task<ObservableCollection<StudentAccount>> GetItemsAsync(bool forceRefresh = false)
        {
            var result = await _client.GetStringAsync(URL);
            StudentAccounts = JsonConvert.DeserializeObject<ObservableCollection<StudentAccount>>(result);
            return StudentAccounts;
        }
        public static async Task<ObservableCollection<StudentAccount>> GetStudentAccountAsync(string Username)
        {
            var result = await _client.GetStringAsync(URL + "/" + Username);
            StudentAccounts = JsonConvert.DeserializeObject<ObservableCollection<StudentAccount>>(result);
            return StudentAccounts;
        }
        public async Task<bool> AddItemAsync(StudentAccount item)
        {
            StudentAccounts.Add(item);

            return await Task.FromResult(true);
        }

        public static async Task UpdateNoteAsync(StudentAccount student, string Note)
        {
            student.Note = Note;
            var content = JsonConvert.SerializeObject(student);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URL + "/UpdateStudentAccount/" + student.Username, sContent);
        }
        public static async Task UpdateAccountAsync(StudentAccount student)
        {
            var content = JsonConvert.SerializeObject(student);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URL + "/UpdateStudentAccount/" + student.Username, sContent);
        }
        public async static Task<bool> DeleteItemAsync(string Username)
        {
            await _client.GetStringAsync(URI + Username);
            return await Task.FromResult(true);
        }

       
    }
}
