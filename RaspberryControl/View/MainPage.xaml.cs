using Newtonsoft.Json;
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

        public DeviceStatusViewModel DeviceStatusVM { get; set; }
        public LightbulbViewModel LightbulbVM { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(1280, 720);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            this.GetViewModels();
            this.InitializeServices();
        }

        private void GetViewModels()
        {
            this.DeviceStatusVM = DeviceStatusObj.DataContext as DeviceStatusViewModel;
            this.LightbulbVM = LightbulbObj.DataContext as LightbulbViewModel;
        }

        private void InitializeServices()
        {
            Service.Raspberry.Create(this.DeviceStatusVM, this.LightbulbVM);
            this.RaspberryService = Service.Raspberry.Instance;
        }
    }
}
