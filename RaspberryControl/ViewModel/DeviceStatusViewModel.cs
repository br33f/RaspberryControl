using RaspberryControl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspberryControl.ViewModel
{
    public class DeviceStatusViewModel : BaseViewModel
    {
        public DeviceStatusModel Model { get; set; }

        public DeviceStatusViewModel()
        {
            this.Model = new DeviceStatusModel();
            this.Model.PropertyChanged += OnModelPropertyChanged;
        }

        public bool IsTcpEnabled
        {
            get
            {
                return Model.IsTcpEnabled;
            }
            set
            {
                Model.IsTcpEnabled = value;
            }
        }

        public bool IsAllJoynEnabled
        {
            get
            {
                return Model.IsAllJoynEnabled;
            }
            set
            {
                Model.IsAllJoynEnabled = value;
            }
        }

        public bool IsKinectEnabled
        {
            get
            {
                return Model.IsKinectEnabled;
            }
            set
            {
                Model.IsKinectEnabled = value;
            }
        }

        public bool IsMotorEnabled
        {
            get
            {
                return Model.IsMotorEnabled;
            }
            set
            {
                Model.IsMotorEnabled = value;
            }
        }

        public bool IsLedEnabled
        {
            get
            {
                return Model.IsLedEnabled;
            }
            set
            {
                Model.IsLedEnabled = value;
            }
        }

        protected void OnModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }
    }
}
