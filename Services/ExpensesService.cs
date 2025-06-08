using Newtonsoft.Json;
using SDKTemplate.Models;
using SDKTemplate.Views.Espace_SNAP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate.Services
{
    class ExpensesService
    {
        public static ObservableCollection<Expense> GroupStudents;
        private const string URL = "http://localhost/blog/public/api/expenses";
        static HttpClient _client = new HttpClient();
        public static async Task OnAdd(string Username, float Amount, string Cause)
        {
            Expense expense = new Expense
            {
                Username = Username,
                Amount = Amount,
                Cause = Cause,
            };
            var content = JsonConvert.SerializeObject(expense);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URL, sContent);
        }

        public static async Task<ObservableCollection<Expense>> GetItemsAsync(bool forceRefresh = false)
        {
            var result = await _client.GetStringAsync(URL);
            GroupStudents = JsonConvert.DeserializeObject<ObservableCollection<Expense>>(result);
            return GroupStudents;
        }
        public async static Task<bool> DeleteItemAsync(ChangeStudent student)
        {
            await _client.GetStringAsync(URL + "/DeleteExpenses/" + student.id);
            return await Task.FromResult(true);
        }
    }
}
