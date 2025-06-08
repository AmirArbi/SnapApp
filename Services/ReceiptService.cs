using Newtonsoft.Json;
using SDKTemplate.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace SDKTemplate.Services
{
    class ReceiptService
    {
        public static ObservableCollection<Receipt> Receipts;
        private const string URL = "http://localhost/blog/public/api/receipt";
        private const string URI = "https://localhost/blog/public/api/receipt/DeleteReceipt/";
        static HttpClient _client = new HttpClient();
        public static async Task OnAdd(string StudentName, string ProfName, string GroupName, float PaidMoney, string PaymentMethod, string Note, string MonthName, string SchoolYear, Visibility Visibility)
        {
            Receipt account = new Receipt
            {
                ProfName = ProfName,
                StudentName = StudentName,
                GroupName = GroupName,
                PaidMoney = PaidMoney,
                PaiementMethod = PaymentMethod,
                Note = Note,
                MonthName = MonthName,
                SchoolYear = SchoolYear,
                Visibility  = Visibility,
            };
            var content = JsonConvert.SerializeObject(account);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URL, sContent);
        }

        public static async Task<ObservableCollection<Receipt>> GetItemsAsync(bool forceRefresh = false)
        {
            var result = await _client.GetStringAsync(URL);
            Receipts = JsonConvert.DeserializeObject<ObservableCollection<Receipt>>(result);
            return Receipts;
        }
        public static async Task<ObservableCollection<Receipt>> GetStudentAsync(string ProfName, string StudentName,string GroupName, string SchoolYear, string MonthName)
        {
            var result = await _client.GetStringAsync(URL + "/" + ProfName + "/" + StudentName + "/" + GroupName + "/" + SchoolYear + "/" + MonthName);
            Receipts = JsonConvert.DeserializeObject<ObservableCollection<Receipt>>(result);
            return Receipts;
        }
        public static async Task<ObservableCollection<Receipt>> GetMonthPayment(string ProfName, string SchoolYear, string MonthName)
        {
            var result = await _client.GetStringAsync(URL + "/month/" + ProfName + "/" + SchoolYear + "/" + MonthName);
            Receipts = JsonConvert.DeserializeObject<ObservableCollection<Receipt>>(result);
            return Receipts;
        }
        public static async Task<ObservableCollection<Receipt>> GetAllStudentPaymentAsync(string ProfName, string StudentName, string SchoolYear)
        {
            var result = await _client.GetStringAsync(URL + "/student/" + ProfName + "/" + StudentName  + "/" + SchoolYear);
            Receipts = JsonConvert.DeserializeObject<ObservableCollection<Receipt>>(result);
            return Receipts;
        }
        public async Task<bool> AddItemAsync(Receipt item)
        {
            Receipts.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Receipt item)
        {
            var oldItem = Receipts.Where((Receipt arg) => arg.ProfName == item.ProfName).FirstOrDefault();
            Receipts.Remove(oldItem);
            Receipts.Add(item);
            return await Task.FromResult(true);
        }
        public async static Task<bool> DeleteItemAsync(string ProfName, string StudentName)
        {
            await _client.GetStringAsync(URI + ProfName + '/' + StudentName);
            return await Task.FromResult(true);
        }

        public async Task<Receipt> GetItemAsync(string ProfName, string StudentName)
        {
            return await Task.FromResult(Receipts.FirstOrDefault(s => s.ProfName == ProfName));
        }
    }
}
