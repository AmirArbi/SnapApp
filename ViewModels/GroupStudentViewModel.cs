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
    class GroupStudentViewModel
    {
        public ObservableCollection<GroupStudents> GroupStudents { get; set; }

        public GroupStudentViewModel()
        {
            GroupStudents = new ObservableCollection<GroupStudents>();
        }
        public async Task ExecuteAddItemCommand(string GroupName, string StudentName,string ProfName, long studentId)
        {
            await GroupStudentsService.OnAdd(GroupName, "2020-2021", StudentName, ProfName, studentId);
        }
        public async Task ExecuteUpdateNoteCommand(GroupStudents student, string Note)
        {
            await GroupStudentsService.UpdateNote(student, Note);
        }
        public async Task ExecuteDeleteSessionCommand(GroupSession session)
        {
            await GroupStudentsService.DeleteSession(session);
        }
        public async Task ExecuteUpdateReductionCommand(GroupStudents student, float Note)
        {
            await GroupStudentsService.UpdateReduction(student, Note);
        }
        public async Task ExecuteUpdateItemCommand(GroupStudents student, string ProfName, string SchoolYear, string GroupName, long studentId)
        {
            await GroupStudentsService.UpdateStudent(student, ProfName, SchoolYear, GroupName, studentId);
        }
        public async Task ExecuteDeleteItemCommand(GroupStudents student)
        {
            await GroupStudentsService.DeleteItemAsync(student);
        }
        public async Task ExecuteLoadItemsCommand()
        {
            ObservableCollection<GroupStudents> events = await GroupStudentsService.GetItemsAsync();
            GroupStudents.Clear();
            foreach (var evnt in events)
            {
                GroupStudents.Add(evnt);
            }
        }
        public async Task ExecuteGetGroupCommand(string Profname, string GroupName, string SchoolYear)
        {
            ObservableCollection<GroupStudents> events = await GroupStudentsService.GetStudentAsync(Profname, GroupName, SchoolYear);
            GroupStudents.Clear();
            foreach (var evnt in events)
            {
                GroupStudents.Add(evnt);
            }
        }
        public async Task ExecuteGetStudentsGroupCommand(string StudentName, string SchoolYear)
        {
            ObservableCollection<GroupStudents> events = await GroupStudentsService.GetStudentGroups(StudentName, SchoolYear);
            GroupStudents.Clear();
            foreach (var evnt in events)
            {
                GroupStudents.Add(evnt);
            }
        }
        public async Task ExecuteGetStudentsGroupCommand(string ProfName, string StudentName, string SchoolYear)
        {
            ObservableCollection<GroupStudents> events = await GroupStudentsService.GetStudentGroup(ProfName, StudentName, SchoolYear);
            GroupStudents.Clear();
            foreach (var evnt in events)
            {
                GroupStudents.Add(evnt);
            }
        }

        public async Task ExecuteDetailCommand(string ProfName, string GroupName, string SchoolYear)
        {
            ObservableCollection<GroupStudents> events = await GroupStudentsService.GetDetail(ProfName, GroupName, SchoolYear);
            GroupStudents.Clear();
            foreach (var evnt in events)
            {
                GroupStudents.Add(evnt);
            }
        }
    }
}
