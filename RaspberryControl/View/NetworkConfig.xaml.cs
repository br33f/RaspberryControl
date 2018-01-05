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

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RaspberryControl.View
{
    public sealed partial class NetworkConfig : ContentDialog
    {
        private string _RaspberryAddress = "192.168.0.22";
        private string _KinectAddress = "192.168.0.20";

        public string RaspberryAddress
        {
            get
            {
                return this._RaspberryAddress;
            }
            set
            {
                this._RaspberryAddress = value;
            }
        }

        public string KinectAddress
        {
            get
            {
                return this._KinectAddress;
            }
            set
            {
                this._KinectAddress = value;
            }
        }

        public NetworkConfig()
        {
            this.InitializeComponent();
        }
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

    }
}
