using SDKTemplate.Commun;
using SDKTemplate.Models;
using SDKTemplate.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Input;

namespace SDKTemplate.ViewModels
{
    class ProfAccountViewModel : BaseViewModel
    {
        public ObservableCollection<ProfAccount> ProfAccounts { get; set; }
        public ProfAccountViewModel()
        {
            ProfAccounts = new ObservableCollection<ProfAccount>();
            //DeleteCommand = new RelayCommand(async (string slug) => await ExecuteDeleteCommand(slug));
        }
        public async static Task ExecuteDeleteCommand(string Username)
        {
            await ProfAccountService.DeleteItemAsync(Username);
        }

        public async Task ExecuteAddItemCommand(string Username, string Name, string Surname, string Password, string Formule, string Subject, string Email)
        {
            await ProfAccountService.OnAdd(Username, Name, Surname, Password, Formule, Subject, Email);
        }
        public async Task ExecuteUpdateItemCommand(string Username, string Name, string Surname, string Password, string Formule, string Subject, string Email)
        {
            await ProfAccountService.UpdateProf(Username, Name, Surname, Password, Formule, Subject, Email);
        }
        public async Task ExecuteLoadItemsCommand()
        {
            ObservableCollection<ProfAccount> events = await ProfAccountService.GetItemsAsync();
            ProfAccounts.Clear();
            foreach (var evnt in events)
            {
                ProfAccounts.Add(evnt);
            }

        }
        public async Task ExecuteGetProfAccountCommand(string Username)
        {
            ObservableCollection<ProfAccount> events = await ProfAccountService.GetProfAccountAsync(Username);
            ProfAccounts.Clear();
            foreach (var evnt in events)
            {
                ProfAccounts.Add(evnt);
            }

        }
    }
}
