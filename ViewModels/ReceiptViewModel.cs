using SDKTemplate.Models;
using SDKTemplate.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace SDKTemplate.ViewModels
{
    class ReceiptViewModel
    {
        public ObservableCollection<Receipt> receipts { get; set; }

        public ReceiptViewModel()
        {
            receipts = new ObservableCollection<Receipt>();
        }
        public async Task ExecuteAddItemCommand(string StudentName, string ProfName,string GroupName, float PaidMoney, string PaymentMethod, string Note,string MonthName, string SchoolYear, Visibility Visibility)
        {
            await ReceiptService.OnAdd(StudentName, ProfName, GroupName, PaidMoney, PaymentMethod, Note, MonthName, SchoolYear, Visibility);
        }

        public async Task ExecuteLoadItemsCommand()
        {
            ObservableCollection<Receipt> events = await ReceiptService.GetItemsAsync();
            receipts.Clear();
            foreach (var evnt in events)
            {
                receipts.Add(evnt);
            }
        }
        public async Task ExecuteGetStudentPaymentCommand(string Profname, string StudentName,string GroupName, string SchoolYear, string MonthName)
        {
            ObservableCollection<Receipt> events = await ReceiptService.GetStudentAsync(Profname, StudentName,GroupName, SchoolYear, MonthName);
            receipts.Clear();
            foreach (var evnt in events)
            {
                receipts.Add(evnt);
            }
        }
        public async Task ExecuteMonthPaymentCommand(string Profname, string SchoolYear, string MonthName)
        {
            ObservableCollection<Receipt> events = await ReceiptService.GetMonthPayment(Profname, SchoolYear, MonthName);
            receipts.Clear();
            foreach (var evnt in events)
            {
                receipts.Add(evnt);
            }
        }
        public async Task ExecuteGetAllStudentPaymentCommand(string Profname, string StudentName, string SchoolYear)
        {
            ObservableCollection<Receipt> events = await ReceiptService.GetAllStudentPaymentAsync(Profname, StudentName, SchoolYear);
            receipts.Clear();
            foreach (var evnt in events)
            {
                receipts.Add(evnt);
            }
        }
    }
}
