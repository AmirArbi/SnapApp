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
    class SchoolYearViewModel
    {
        public ObservableCollection<Schoolyear> years { get; set; }

        public SchoolYearViewModel()
        {
            years = new ObservableCollection<Schoolyear>();
        }
        public async Task ExecuteAddItemCommand(string SchoolYear, DateTime Begining, DateTime Ending)
        {
            await SchoolYearService.OnAdd(SchoolYear, Begining, Ending);
        }
        /*public async Task ExecuteUpdateNumberOfStudents(ProfGroup profGroup)
        {
            await MonthService.UpdateNumberOfStudents(profGroup);
        }*/

        public async Task ExecuteLoadItemsCommand()
        {
            ObservableCollection<Schoolyear> events = await SchoolYearService.GetItemsAsync();
            years.Clear();
            foreach (var evnt in events)
            {
                years.Add(evnt);
            }
        }
    }
}
