﻿using Newtonsoft.Json;
using RaspberryControl.View;
using RaspberryControl.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace RaspberryControl
{
    public sealed partial class MainPage : Page
    {
        public const string INPUT_SOCK_PORT = "8725";

        public Service.Raspberry RaspberryService { get; set; }
        public Service.Kinect KinectService { get; set; }

        public DeviceStatusViewModel DeviceStatusVM { get; set; }
        public LightbulbViewModel LightbulbVM { get; set; }
        public MotorViewModel MotorAVM { get; set; }
        public MotorViewModel MotorBVM { get; set; }
        public KinectViewModel KinectVM { get; set; }
        public TemperatureSensorViewModel TemperatureSensorVM { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(1280, 820);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            this.GetViewModels();
            this.InitializeViewModels();

            Loaded += MainPage_Loaded;
        }

        private void GetViewModels()
        {
            this.DeviceStatusVM = DeviceStatusObj.DataContext as DeviceStatusViewModel;
            this.LightbulbVM = LightbulbObj.DataContext as LightbulbViewModel;
            this.MotorAVM = MotorAObj.DataContext as MotorViewModel;
            this.MotorBVM = MotorBObj.DataContext as MotorViewModel;
            this.KinectVM = KinectObj.DataContext as KinectViewModel;
            this.TemperatureSensorVM = MiscObj.DataContext as TemperatureSensorViewModel;
        }

        private void InitializeViewModels()
        {
            this.MotorAVM.initializeMotorModel("A");
            this.MotorBVM.initializeMotorModel("B");
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var networkConfigDialog = new NetworkConfig();
            var result = await networkConfigDialog.ShowAsync();
            InitializeServices(networkConfigDialog.RaspberryAddress, networkConfigDialog.KinectAddress);
        }

        private void InitializeServices(string raspberryAddress, string kinectAddress)
        {
            Service.Raspberry.Create(raspberryAddress, this.DeviceStatusVM, this.LightbulbVM, this.TemperatureSensorVM);
            this.RaspberryService = Service.Raspberry.Instance;

            Service.Kinect.Create(kinectAddress, this.DeviceStatusVM, this.LightbulbVM, this.MotorAVM, this.MotorBVM, this.KinectVM);
            this.KinectService = Service.Kinect.Instance;
        }
    }
}
