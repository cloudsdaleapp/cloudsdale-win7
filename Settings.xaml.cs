﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Cloudsdale_Win7.Models;
using Newtonsoft.Json.Linq;

namespace Cloudsdale_Win7
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
            name.Text = MainWindow.User["user"]["name"].ToString();
            username.Text = MainWindow.User["user"]["username"].ToString();
            if (UserModel.NameChangesAllowed() != 1)
            {
                username.IsEnabled = false;
            }
            skype.Text = UserModel.SkypeUsername(MainWindow.User["user"]["id"].ToString());
            email.Text = MainWindow.User["user"]["email"].ToString();
            akaList.Text = UserModel.AkaList(MainWindow.User["user"]["id"].ToString());
        }

        private void ClearPassword(object sender, MouseButtonEventArgs e)
        {
            password.Password = "";
        }
    }
}
