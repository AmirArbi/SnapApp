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
    class ProfAccountService
    {
        public static ObservableCollection<ProfAccount> profAccounts;
        private const string URL = "http://localhost/blog/public/api/prof-accounts";
        private const string URI = "http://localhost/blog/public/api/prof-accounts/DeleteProfAccount/";
        static HttpClient _client = new HttpClient();
        public static async Task OnAdd(string Username, string Name, string Surname, string Password,string Formule, string Subject, string Email)
        {
            ProfAccount account = new ProfAccount
            {
                Username = Username,
                Name = Name,
                Surname = Surname,
                Password = Password,
                Formule = Formule,
                Subject = Subject,
                Email = Email,
            };
            var content = JsonConvert.SerializeObject(account);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URL, sContent);
        }
        public static async Task UpdateProf(string Username, string Name, string Surname, string Password, string Formule, string Subject, string Email)
        {
            ProfAccount account = new ProfAccount
            {
                Username = Username,
                Name = Name,
                Surname = Surname,
                Password = Password,
                Formule = Formule,
                Subject = Subject,
                Email = Email,
            };
            var content = JsonConvert.SerializeObject(account);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URL + "/UpdateProfAccount/" + Username, sContent);
        }
        public static async Task<ObservableCollection<ProfAccount>> GetItemsAsync(bool forceRefresh = false)
        {
            var result = await _client.GetStringAsync(URL);
            profAccounts = JsonConvert.DeserializeObject<ObservableCollection<ProfAccount>>(result);
            return profAccounts;
        }
        public static async Task<ObservableCollection<ProfAccount>> GetProfAccountAsync(string Username)
        {
            var result = await _client.GetStringAsync(URL + "/" + Username);
            profAccounts = JsonConvert.DeserializeObject<ObservableCollection<ProfAccount>>(result);
            return profAccounts;
        }
        public async Task<bool> AddItemAsync(ProfAccount item)
        {
            profAccounts.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(ProfAccount item)
        {
            var oldItem = profAccounts.Where((ProfAccount arg) => arg.Username == item.Username).FirstOrDefault();
            profAccounts.Remove(oldItem);
            profAccounts.Add(item);
            return await Task.FromResult(true);
        }
        public async static Task<bool> DeleteItemAsync(string Username)
        {
            //var oldItem = events.Where((Event arg) => arg.Slug == slug).FirstOrDefault();
            //events.Remove(oldItem);
            await _client.GetStringAsync(URI  + Username);
            return await Task.FromResult(true);
        }

        public async Task<ProfAccount> GetItemAsync(string Username)
        {
            return await Task.FromResult(profAccounts.FirstOrDefault(s => s.Username == Username));
        }

    }
}

