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
    class ProfGroupService
    {
        public static ObservableCollection<ProfGroup> ProfGroups;
        private const string URL = "http://localhost/blog/public/api/prof-groups";
        private const string URI = "http://localhost/blog/public/api/prof-groups/DeleteProfGroup/";
        private const string URU = "http://localhost/blog/public/api/prof-groups/UpdateProfGroup/";
        static HttpClient _client = new HttpClient();
        public async static Task OnAdd(string ProfName, string SchoolYear, string GroupName, string Level, int NomberOfStudents)
        {
            ProfGroup account = new ProfGroup
            {
                ProfName = ProfName,
                GroupName = GroupName,
                Level = Level,
                NumberOfStudents = NomberOfStudents,
                SchoolYear = SchoolYear,
            };
            var content = JsonConvert.SerializeObject(account);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URL, sContent);
        }

        public static async Task<ObservableCollection<ProfGroup>> GetItemsAsync(bool forceRefresh = false)
        {
            var result = await _client.GetStringAsync(URL);
            ProfGroups = JsonConvert.DeserializeObject<ObservableCollection<ProfGroup>>(result);
            return ProfGroups;
        }
        public static async Task<ObservableCollection<ProfGroup>> GetProfGroupAsync(string ProfGroup, string SchoolYear)
        {
            var result = await _client.GetStringAsync(URL + "/" + ProfGroup + "/" + SchoolYear);
            ProfGroups = JsonConvert.DeserializeObject<ObservableCollection<ProfGroup>>(result);
            return ProfGroups;
        }
        public async Task<bool> AddItemAsync(ProfGroup item)
        {
            ProfGroups.Add(item);

            return await Task.FromResult(true);
        }

        public async static Task UpdateNumberOfStudents(ProfGroup item)
        {
            item.NumberOfStudents++;
            var content = JsonConvert.SerializeObject(item);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URU + item.ProfName + "/" + item.GroupName +  "/" + item.SchoolYear , sContent);
        }
        public async static Task UpdateMinus(ProfGroup item)
        {
            item.NumberOfStudents--;
            var content = JsonConvert.SerializeObject(item);
            var sContent = new StringContent(content, Encoding.UTF8, "application/json");
            var b = await _client.PostAsync(URU + item.ProfName + "/" + item.GroupName + "/" + item.SchoolYear, sContent);
        }
        public async static Task<bool> DeleteItemAsync(string ProfName, string GroupName, string SchoolYear)
        {
            await _client.GetStringAsync(URI + ProfName + '/' + GroupName + '/' + SchoolYear);
            return await Task.FromResult(true);
        }
    }
}
