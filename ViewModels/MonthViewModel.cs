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
    class MonthViewModel
    {
        public ObservableCollection<Month> Months { get; set; }

        public MonthViewModel()
        {
            Months = new ObservableCollection<Month>();
        }
        public async Task ExecuteAddItemCommand(string GroupName, string Slug, string ProfName, string MonthName, long NumberSession, float WantedMoney)
        {
            await MonthService.OnAdd(GroupName, Slug, ProfName, MonthName, NumberSession, "2020-2021", WantedMoney);
        }
        public async Task ExecuteUpdateMonthCommand(Month month)
        {
            await MonthService.UpdateMonthSession(month);
        }
        public async Task ExecuteUpdateNumberOfStudents(Month month)
        {
            await MonthService.UpdateCurrentSession(month);
        }

        public async Task ExecuteLoadItemsCommand()
        {
            ObservableCollection<Month> events = await MonthService.GetItemsAsync();
            Months.Clear();
            foreach (var evnt in events)
            {
                Months.Add(evnt);
            }
        }
        public async Task ExecuteGetGroupByProfnameCommand(string Profname, string SchoolYear)
        {
            ObservableCollection<Month> events = await MonthService.GetStudentAsync(Profname, SchoolYear);
            Months.Clear();
            foreach (var evnt in events)
            {
                Months.Add(evnt);
            }
        }
        public async Task ExecuteGetGroupByProfnameCommand(string Profname, string GroupName, string SchoolYear)
        {
            ObservableCollection<Month> events = await MonthService.GetStudentAsync(Profname, GroupName, SchoolYear);
            Months.Clear();
            foreach (var evnt in events)
            {
                Months.Add(evnt);
            }
        }
        public async Task ExecuteMonthCommand(string Profname, string GroupName, string Slug, string SchoolYear)
        {
            ObservableCollection<Month> events = await MonthService.GetStudentAsync(Profname, GroupName, Slug, SchoolYear);
            Months.Clear();
            foreach (var evnt in events)
            {
                Months.Add(evnt);
            }
        }
        public async Task ExecuteDetailCommand(string Profname, string StudentName, string GroupName, string SchoolYear)
        {
            ObservableCollection<Month> events = await MonthService.GetDetailAsync(Profname, StudentName, GroupName, SchoolYear);
            Months.Clear();
            foreach (var evnt in events)
            {
                Months.Add(evnt);
            }
        }
    }
}
