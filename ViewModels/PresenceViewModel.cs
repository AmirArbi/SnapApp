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
    class PresenceViewModel
    {
        public ObservableCollection<Presence> presences { get; set; }

        public PresenceViewModel()
        {
            presences = new ObservableCollection<Presence>();
        }
        public async Task ExecuteAddItemCommand(string StudentName, string ProfName, string GroupName, string SchoolYear, string MonthName, long session_id, bool IsPresent)
        {
            await PresenceService.OnAdd(StudentName, ProfName, GroupName, SchoolYear, MonthName, session_id, IsPresent);
        }
        public async Task ExecuteUpdatePresenceCommand(string StudentName, string ProfName, string GroupName, string SchoolYear, string MonthName, long session_id, bool IsPresent)
        {
            await PresenceService.UpadtePresence(StudentName, ProfName, GroupName, SchoolYear, MonthName, session_id, IsPresent);
        }

        public async Task ExecuteLoadItemsCommand()
        {
            ObservableCollection<Presence> events = await PresenceService.GetItemsAsync();
            presences.Clear();
            foreach (var evnt in events)
            {
                presences.Add(evnt);
            }
        }
        public async Task ExecuteGetPresenceCommand(string StudentName, string ProfName, string GroupName, string SchoolYear, string MonthName, long session_id)
        {
            ObservableCollection<Presence> events = await PresenceService.GetPresenceAsync(StudentName, ProfName,GroupName, SchoolYear, MonthName, session_id);
            presences.Clear();
            foreach (var evnt in events)
            {
                presences.Add(evnt);
            }
        }
        public async Task ExecuteGetAllPresenceOfStudentAsyncCommand(string StudentName, string ProfName, string SchoolYear)
        {
            ObservableCollection<Presence> events = await PresenceService.GetAllPresenceOfStudentAsync(StudentName, ProfName, SchoolYear);
            presences.Clear();
            foreach (var evnt in events)
            {
                presences.Add(evnt);
            }
        }
    }
}
