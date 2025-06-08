using SDKTemplate.Models;
using SDKTemplate.Services;
using SDKTemplate.Views.Espace_SNAP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate.ViewModels
{
    class ExpensesViewModel
    {
        public ObservableCollection<Expense> sessions { get; set; }

        public ExpensesViewModel()
        {
            sessions = new ObservableCollection<Expense>();
        }
        public async Task ExecuteAddItemCommand(string Username, float Amount, string Cause)
        {
            await ExpensesService.OnAdd(Username, Amount, Cause);
        }

        public async Task ExecuteLoadItemsCommand()
        {
            ObservableCollection<Expense> events = await ExpensesService.GetItemsAsync();
            sessions.Clear();
            foreach (var evnt in events)
            {
                sessions.Add(evnt);
            }
        }
    }
}
