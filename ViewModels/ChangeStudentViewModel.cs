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
    class ChangeStudentViewModel
    {
        public ObservableCollection<ChangeStudent> studentChangements { get; set; }

        public ChangeStudentViewModel()
        {
            studentChangements = new ObservableCollection<ChangeStudent>();
        }
        public async Task ExecuteAddItemCommand(string OldGroup, string SchoolYear, string StudentName, string ProfName, string MonthName, int CurrentSession)
        {
            await ChangeStudentService.OnAdd(OldGroup, SchoolYear, StudentName, ProfName, MonthName, CurrentSession);
        }

        public async Task ExecuteLoadItemsCommand()
        {
            ObservableCollection<ChangeStudent> events = await ChangeStudentService.GetItemsAsync();
            studentChangements.Clear();
            foreach (var evnt in events)
            {
                studentChangements.Add(evnt);
            }
        }
        public async Task ExecuteGetChangementCommand(string StudentName, string ProfName, string OldGroup, string SchoolYear, string MonthName)
        {
            ObservableCollection<ChangeStudent> events = await ChangeStudentService.GetChangementAsync( StudentName,  ProfName,  OldGroup,  SchoolYear,  MonthName);
            studentChangements.Clear();
            foreach (var evnt in events)
            {
                studentChangements.Add(evnt);
            }
        }
    }
}
