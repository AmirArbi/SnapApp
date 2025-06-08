using SDKTemplate.Models;
using SDKTemplate.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate.ViewModels
{
    class EmployerViewModel
    {
        public ObservableCollection<Employer> employers { get; set; }

        public EmployerViewModel()
        {
            employers = new ObservableCollection<Employer>();
        }
        public async Task ExecuteAddItemCommand(string EmployerName, string Name, string Surname, string Role)
        {
            await EmployerService.OnAdd(EmployerName, Name, Surname, Role);
        }

        public async Task ExecuteLoadItemsCommand()
        {
            ObservableCollection<Employer> events = await EmployerService.GetItemsAsync();
            employers.Clear();
            foreach (var evnt in events)
            {
                employers.Add(evnt);
            }
        }
    }
}
