using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate.Views.Espace_Prof.Update
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class ProfInfo : Page
    {
        public ProfInfo()
        {
            this.InitializeComponent();
            Name.Text = ProfDetail.myData.Name;
            Surname.Text = ProfDetail.myData.Surname;
            Email.Text = ProfDetail.myData.Email;
            Subject.Text = ProfDetail.myData.Subject;
            Formule.Text = ProfDetail.myData.Formule;
        }

        private void Retouner_Click(object sender, RoutedEventArgs e)
        {
            string par = JsonConvert.SerializeObject(ProfDetail.myData);
            Frame.Navigate(typeof(ProfDetail), par);
        }
    }
}
