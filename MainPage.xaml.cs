//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

using Newtonsoft.Json;
using SDKTemplate.Models;
using SDKTemplate.Views;
using SDKTemplate.Views.Espace_Eléve;
using SDKTemplate.Views.Espace_Prof;
using SDKTemplate.Views.Espace_SNAP;
using SDKTemplate.Views.info;
using SDKTemplate.Views.Notifications;
using SDKTemplate.Views.Reglage;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Gaming.Input;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage Current;
        public MainPage()
        {
            this.InitializeComponent();
            Current = this;
        }

        /// <summary>
        /// Called whenever the user changes selection in the scenarios list.  This method will navigate to the respective
        /// sample scenario page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /*private void ScenarioControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Clear the status block when navigating scenarios.

            ListBox scenarioListBox = sender as ListBox;
            Espace s = scenarioListBox.SelectedItem as Espace;
            if (s != null)
            {
                ScenarioFrame.Navigate(s.ClassType);
                if (Window.Current.Bounds.Width < 640)
                {
                    Splitter.IsPaneOpen = false;
                }
            }
        }*/
        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>
        {
            ("Expenses", typeof(Expenses)),
            ("EspaceProf", typeof(ProfSearch)),
            ("EspaceStudent", typeof(StudentSearch)),
            ("EspaceSanp", typeof(PasswordView)),
            ("MettreEnVielle", typeof(PasswordView)),
            ("Info", typeof(InfoView)),
        };

        public List<Espace> Scenarios
        {
            get { return this.espaces; }
        }

        /// <summary>
        /// Display a message to the user.
        /// This method may be called from any thread.
        /// </summary>
        /// <param name="strMessage"></param>
        /// <param name="type"></param>

        async void Footer_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri(((HyperlinkButton)sender).Tag.ToString()));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Splitter.IsPaneOpen = !Splitter.IsPaneOpen;
        }

        public void Navigate(Type page)
        {
            CurrentPage.Navigate(page);
        }
        private void nvSample5_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            try
            {
                if (args.IsSettingsSelected == true)
                {
                    Expense verouillage = new Expense
                    {
                        AdminName = "Directeur",
                        Username = "Settings"
                    };
                    string par = JsonConvert.SerializeObject(verouillage);
                    CurrentPage.Navigate(typeof(PasswordView), par);
                }
                else if (args.SelectedItemContainer != null)
                {
                    var navItemTag = args.SelectedItemContainer.Tag.ToString();
                    for (int i = 0; i < _pages.Count; i++)
                    {
                        if(navItemTag == _pages[i].Tag)
                        {
                            if (navItemTag == "EspaceSanp")
                            {
                                Expense expense = new Expense
                                {
                                    AdminName = "Directeur"
                                };
                                string par = JsonConvert.SerializeObject(expense);
                                CurrentPage.Navigate(_pages[i].Page, par);
                            } else if (navItemTag == "EspaceProf") {
                                bool ChangeGroup = false;
                                string par = JsonConvert.SerializeObject(ChangeGroup);
                                CurrentPage.Navigate(_pages[i].Page, par);
                            }
                            else if (navItemTag == "MettreEnVielle")
                            {
                                Expense verouillage = new Expense
                                {
                                    AdminName = "Administration",
                                    Username = "App"
                                };
                                string par = JsonConvert.SerializeObject(verouillage);
                                Frame .Navigate(_pages[i].Page, par);
                            }
                            else
                                CurrentPage.Navigate(_pages[i].Page);
                        }
                    }
                }
            } catch (Exception)
            {

            }
        }
    }
    public enum NotifyType
    {
        StatusMessage,
        ErrorMessage
    };
}
