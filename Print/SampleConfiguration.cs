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

using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using PrintSample;
using SDKTemplate.Views.Espace_Prof;
using SDKTemplate.Views.Notifications;
using SDKTemplate.Views.Espace_Eléve;
using SDKTemplate.Views.Espace_SNAP;
using SDKTemplate.Views.Reglage;
using SDKTemplate.Views.info;

namespace SDKTemplate
{
    public partial class MainPage : Page
    {

        List<Espace> espaces = new List<Espace>
        {
            new Espace() { ImageSource="Images/Notifications.png", Title="Notification" , ClassType=typeof(StudentSearch)},
            new Espace() { ImageSource="Images/enseignant.png", Title="Espace Prof" ,ClassType=typeof(ProfSearch)},
            new Espace() { ImageSource="Images/Student.png", Title="Espace Eléve" ,ClassType=typeof(StudentSearch)},
            new Espace() { ImageSource="Images/Centre.png", Title="Espace SNAP" ,ClassType=typeof(SNAPmenu)},
            new Espace() { ImageSource="Images/Reglage.png", Title="Paramétre" ,ClassType=typeof(ReglageView)},
            new Espace() { ImageSource="Images/about.png", Title="Info" ,ClassType=typeof(InfoView)}
        };
    }

    public class Espace
    {
        public string ImageSource { get; set; }
        public string Title { get; set; }
        public Type ClassType { get; set; }
    }
}