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
    public class GroupSessionViewModel
    {
        public ObservableCollection<GroupSession> sessions { get; set; }

        public GroupSessionViewModel()
        {
            sessions = new ObservableCollection<GroupSession>();
        }
        public async Task ExecuteAddItemCommand(string GroupName, string ProfName, string StudentName, DateTime Date, long session_id, string MonthName, string SchoolYear)
        {
            await GroupSessionService.OnAdd(GroupName, ProfName, StudentName, Date, session_id, MonthName, SchoolYear);
        }

        public async Task ExecuteUpdateSessionCommand(GroupSession session)
        {
            await GroupSessionService.UpdateSession(session);
        }
        public async Task ExecuteUpdateAllCommand(string ProfName, string GroupName, string SchoolYear, string MonthName, long session_id)
        {
            await GroupSessionService.UpdateAll(ProfName, GroupName, SchoolYear, MonthName, session_id);
        }
        public async Task ExecuteLoadItemsCommand()
        {
            ObservableCollection<GroupSession> events = await GroupSessionService.GetItemsAsync();
            sessions.Clear();
            foreach (var evnt in events)
            {
                sessions.Add(evnt);
            }
        }
        public async Task ExecuteLoadPresence(string Profname, string GroupName, string SchoolYear, string MonthName, long session_id)
        {
            ObservableCollection<GroupSession> events = await GroupSessionService.LoadPresence(Profname, GroupName, SchoolYear, MonthName, session_id);
            sessions.Clear();
            foreach (var evnt in events)
            {
                sessions.Add(evnt);
            }
        }
        public async Task ExecuteGetSession(string Profname, string GroupName,string StudentName, string SchoolYear, string MonthName, long session_id)
        {
            ObservableCollection<GroupSession> events = await GroupSessionService.GetSession(Profname, GroupName, StudentName, SchoolYear, MonthName, session_id);
            sessions.Clear();
            foreach (var evnt in events)
            {
                sessions.Add(evnt);
            }
        }
        public async Task ExecuteGetGroupByProfnameCommand(string Profname, string GroupName, string MonthName)
        {
            ObservableCollection<GroupSession> events = await GroupSessionService.GetSessionAsync(Profname, GroupName, MonthName);
            sessions.Clear();
            foreach (var evnt in events)
            {
                sessions.Add(evnt);
            }
        }
        public async Task ExecuteGetStudentSessionsPerMonth(string StudentName, string ProfName, string GroupName,string MonthName)
        {
            ObservableCollection<GroupSession> events = await GroupSessionService.GetStudentSession(StudentName, ProfName, GroupName, MonthName);
            sessions.Clear();
            foreach (var evnt in events)
            {
                sessions.Add(evnt);
            }
        }
        public async Task ExecuteGetStudentSessions(string StudentName, string ProfName)
        {
            ObservableCollection<GroupSession> events = await GroupSessionService.GetStudentSession(StudentName, ProfName);
            sessions.Clear();
            foreach (var evnt in events)
            {
                sessions.Add(evnt);
            }
        }
    }
}
