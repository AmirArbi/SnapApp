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
    class ProfGroupViewModel
    {
        public ObservableCollection<ProfGroup> ProfGroups { get; set; }

        public ProfGroupViewModel()
        {
            ProfGroups = new ObservableCollection<ProfGroup>();
        }
        public async Task ExecuteAddItemCommand(string ProfName, string GroupName, string Level)
        {
            await ProfGroupService.OnAdd(ProfName, "2020-2021", GroupName, Level, 0);
        }
        public async Task ExecuteUpdateNumberOfStudents(ProfGroup profGroup)
        {
            await ProfGroupService.UpdateNumberOfStudents(profGroup);
        }
        public async Task ExecuteUpdateMinus(ProfGroup profGroup)
        {
            await ProfGroupService.UpdateMinus(profGroup);
        }
        public async Task ExecuteLoadItemsCommand()
        {
            ObservableCollection<ProfGroup> events = await ProfGroupService.GetItemsAsync();
            ProfGroups.Clear();
            foreach (var evnt in events)
            {
                ProfGroups.Add(evnt);
            }
        }
        public async Task ExecuteGetGroupByProfnameCommand(string Profname, string SchoolYear)
        {
            ObservableCollection<ProfGroup> events = await ProfGroupService.GetProfGroupAsync(Profname,SchoolYear);
            ProfGroups.Clear();
            foreach (var evnt in events)
            {
                ProfGroups.Add(evnt);
            }
        }
    }
}
