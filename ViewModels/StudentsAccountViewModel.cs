using SDKTemplate.Models;
using SDKTemplate.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace SDKTemplate.ViewModels
{
    
    class StudentsAccountViewModel
    {
        public ObservableCollection<StudentAccount> accounts { get; set; }
        public StudentsAccountViewModel()
        {
            accounts = new ObservableCollection<StudentAccount>();
        }
        public async Task ExecuteAddItemCommand(string Username, string Email, string Password, string School, string Name, string Surname, string FeedBack, string Avrage, string PhoneNumber, ImageSource Image,string Gender)
        {
            await StudentAccountService.OnAdd( Username,  Email,  Password,  School,  Name,  Surname,  FeedBack,  Avrage,  PhoneNumber,  Image, Gender);
        }

        public async Task ExecuteUpadteNoteCommand(StudentAccount account, string Note)
        {
            await StudentAccountService.UpdateNoteAsync(account, Note);
        }

        public async Task ExecuteUpadteAccountCommand(StudentAccount account)
        {
            await StudentAccountService.UpdateAccountAsync(account);
        }
        public async Task ExecuteLoadItemsCommand()
        {
            ObservableCollection<StudentAccount> events = await StudentAccountService.GetItemsAsync();
            accounts.Clear();
            foreach (var evnt in events)
            {
                accounts.Add(evnt);
            }
        }

        public async Task<StudentAccount> ExecuteGetStudentAccountCommand(string Username)
        {
            ObservableCollection<StudentAccount> events = await StudentAccountService.GetStudentAccountAsync(Username);
            StudentAccount account = new StudentAccount();
            foreach (var evnt in events)
            {
                account = evnt;
            }
            return account;
        }
    }
}
