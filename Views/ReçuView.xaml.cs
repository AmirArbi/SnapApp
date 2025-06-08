using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SDKTemplate.Models;
using SDKTemplate.ViewModels;
using System.Globalization;
using Newtonsoft.Json;
using System.Threading.Tasks;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate.Code
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class ReçuView : Page
    {
        ProfAccountViewModel viewModel = new ProfAccountViewModel();
        StudentsAccountViewModel viewModelStudent = new StudentsAccountViewModel();
        private Receipt myData { get; set; }
        
        public ReçuView(Receipt Data)
        {
            this.InitializeComponent();
            //LoadReceipt
            myData = Data;
            Load();
        }

        protected async Task Load()
        {
            Run id = new Run();
            id.Text =  " " + myData.id.ToString();
            Id.Inlines.Add(id);

            Run r = new Run();
            string format = "dddd, MMM dd yyyy HH:mm:ss";
            r.Text = " " + myData.DateTime.ToString(format, new CultureInfo("fr-FR"));
            Date.Inlines.Add(r);

            StudentAccount studentAccount =  await viewModelStudent.ExecuteGetStudentAccountCommand(myData.StudentName);
            Run name = new Run();
            name.Text = " " + studentAccount.Name + " " + studentAccount.Surname;
            FullName.Inlines.Add(name);

            if(myData.ProfName!= "CentreSnap")
            {
                await viewModel.ExecuteGetProfAccountCommand(myData.ProfName);
                Run ProfName = new Run();
                ProfName.Text = " " + viewModel.ProfAccounts[0].Name + " " + viewModel.ProfAccounts[0].Surname;
                Profname.Inlines.Add(ProfName);
            } else
            {
                Profname.Inlines.Clear();
            }

            if (myData.ProfName != "CentreSnap")
            {
                Run subject = new Run();
                subject.Text = " " + viewModel.ProfAccounts[0].Subject;
                Subject.Inlines.Add(subject);
            } 
                
            Run sessions = new Run();
            sessions.Text = " " + myData.MonthName;
            Session.Inlines.Add(sessions);



            Run money = new Run();
            money.Text = " " + myData.PaidMoney.ToString() + " DT";
            Montant.Inlines.Add(money);

            Run pmnt = new Run();
            pmnt.Text = " " + myData.PaiementMethod;
            PaymentMethode.Inlines.Add(pmnt);
        }
    }
}
