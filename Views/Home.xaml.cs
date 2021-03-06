﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using CloudsdaleWin7.lib.CloudsdaleLib.Misc.Screenshot;
using CloudsdaleWin7.lib.Helpers;

namespace CloudsdaleWin7 {
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home
    {
        private readonly string UserName = App.Connection.SessionController.CurrentSession.Name;
        public static Home Instance;
        public Home()
        {
            InitializeComponent();
            Instance = this;
            Welcome.Content = WelcomeMessage(UserName);
            AviImage.Source = new BitmapImage(App.Connection.SessionController.CurrentSession.Avatar.Normal);
            Loaded += (sender, args) => Animate();
            GatherCaptures();
        }
        private static string WelcomeMessage(string name)
        {
            var r = new Random();
            String message;
            switch(r.Next(0,5))
            {
                case 0:
                    message = "Hi, [:name]!";
                    break;
                case 1:
                    message = "Welcome, [:name]!";
                    break;
                case 2:
                    message = "Welcome back, [:name].";
                    break;
                case 3:
                    message = "Hi there, [:name]!";
                    break;
                case 4:
                    if (DateTime.Now.Hour >= 6 && DateTime.Now.Hour < 12) message = "Good morning, [:name]~";
                    else if (DateTime.Now.Hour < 6) message = "It's a lovely night, [:name].";
                    else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 20) message = "Good afternoon [:name].";
                    else message = "Howdy, [:name]~";
                    break;
                default:
                    message = "Hello, [:name].";
                    break;
            }
            return message.Replace("[:name]", name);
        }
        private void Animate()
        {
            #region Welcome Message

            Welcome.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 100, new Duration(new TimeSpan(1000000000))));

            #region FancyLines

            //The line underneath the name first
            Line1.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 100, new Duration(new TimeSpan(1000000000))));
            Line1.BeginAnimation(WidthProperty,
                new DoubleAnimation(0, Welcome.ActualWidth, new Duration(new TimeSpan(0, 0, 1)),
                    FillBehavior.HoldEnd)
                {
                    EasingFunction = new ExponentialEase() {  Exponent = 3 }
                });
            Avi.BeginAnimation(MarginProperty,
                new ThicknessAnimation(new Thickness(0, -1000, 100, 1010), new Thickness(0, 100, 100, 200),
                    new Duration(new TimeSpan(0, 0, 1)))
                {
                    EasingFunction = new BounceEase {Bounces = 3, Bounciness = 4}
                });

            #endregion

            #endregion
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            Line1.Width = MainWindow.Instance.Width - 400;
        }

        private void GatherCaptures()
        {
            foreach (var image in Directory.GetFiles(Screencap.SavedDirectory))
            {
                ScreencapList.Items.Add(image.Split('\\')[image.Split('\\').Count() - 1]);
            }
        }

        private void SwapGrid(object sender, RoutedEventArgs e)
        {
            RootGrid.SwitchVisibility(SecondaryGrid);
        }

        private void OpenImage(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start(Screencap.SavedDirectory + ((ListView) sender).SelectedItem);
        }
    }
}
