﻿using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using CloudsdaleWin7.Views.Notifications;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;

namespace CloudsdaleWin7.Views.Flyouts.CloudFlyouts
{
    /// <summary>
    /// Interaction logic for UserFlyout.xaml
    /// </summary>
    public partial class UserFlyout
    {

        private static User Self { get; set; }
        private static Cloud FoundOn { get; set; }

        public UserFlyout(User user, Cloud cloud, bool isMod)
        {
            InitializeComponent();
            Self = user;
            FoundOn = cloud;
            AvatarBounce();
            Username.Text = "@" + Self.Username;
            Name.Text = user.Name;
            AviImage.Source = new BitmapImage(Self.Avatar.Normal);
            akaList.ItemsSource = Self.AlsoKnownAs;
            BanReason.Text = "Banned by @" + App.Connection.SessionController.CurrentSession.Username;
            AdminUI.Visibility = isMod ? Visibility.Visible : Visibility.Hidden;

            TimeBox.Text = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
            BanCal.SelectedDate = DateTime.Now;
        }

        private void AvatarBounce()
        {
            var a = new ThicknessAnimation(new Thickness(10, -800, 0, 810), new Thickness(10, 10, 0, 0),
                                           new Duration(new TimeSpan(0, 0, 1)));
            var bounce = new BounceEase();
            bounce.Bounces = 3;
            bounce.Bounciness = 10;
            a.EasingFunction = bounce;
            Avi.BeginAnimation(MarginProperty, a);
        }

        private void AddOnSkype(object sender, RoutedEventArgs e)
        {
            UIHelpers.MessageOnSkype(Self.SkypeName ?? Self.SkypeName);
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            Main.Instance.HideFlyoutMenu();
        }

        private void ShowBanUi(object sender, RoutedEventArgs e)
        {
            BanUI.Visibility = BanUI.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }

        private void AttemptBan(object sender, RoutedEventArgs e)
        {
            if (BanCal.SelectedDate == null)
            {
                App.Connection.NotificationController.Notification.Notify(NotificationType.Client,
                                                                          new Message
                                                                              {Content = "You have to select a date!"});
                return;
            }
            var timeReg = new Regex("^(?:[01]?[0-9]|2[0-3]):[0-5][0-9]$");
            if (!timeReg.IsMatch(TimeBox.Text))
            {
                App.Connection.NotificationController.Notification.Notify(NotificationType.Client,
                                                                          new Message
                                                                              {
                                                                                  Content =
                                                                                      "Time format must be HH:MM in 24 hour time!"
                                                                              });
                return;
            }
            if (String.IsNullOrEmpty(BanReason.Text))
            {
                App.Connection.NotificationController.Notification.Notify(NotificationType.Client,
                                                                          new Message
                                                                              {
                                                                                  Content = "Can't leave the reason blank!"
                                                                              });
                return;
            }

            if (MessageBox.Show("Ban @" + Self.Username + " for " + BanReason.Text + " until " + TimeBox.Text + BanCal.SelectedDate + "?", "Confirm", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;
            var client = new HttpClient().AcceptsJson();
            

        }
    }
}
