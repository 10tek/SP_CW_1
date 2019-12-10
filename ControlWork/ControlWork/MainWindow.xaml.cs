﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace ControlWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WindowState prevState;
        private DispatcherTimer timer = new DispatcherTimer();
        private Random random = new Random();
        private int period;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TaskbarIconTrayLeftMouseDown(object sender, RoutedEventArgs e)
        {
            Show();
            WindowState = prevState;    
            taskBar.Visibility = Visibility.Hidden;
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            if (WindowState == WindowState.Normal)
            {
                Hide();
                taskBar.Visibility = Visibility.Visible;
            }
            else
                prevState = WindowState;
        }

        private void SaveBtnClick(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(periodTB.Text, out period))
            {
                MessageBox.Show("Некорректные данные!");
                return;
            }
            timer.Tick += ScreenShot;
            timer.Start();
        }

        private void ScreenShot(object sender, object e)
        {
            var randomTime = random.Next(period * 60) + 1;
            timer.Interval = new TimeSpan(0, 0, 0, randomTime, 0);
            System.Drawing.Image screen = Pranas.ScreenshotCapture.TakeScreenshot(true);
            var path = @$"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\Pictures\ScreenApp";
            Directory.CreateDirectory(path);
            path += @$"\{DateTime.Now.ToString("ddMMyyyy - hhmmss")}.png";
            screen.Save(path);
        }
    }
}
