using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace RaspberryControl.Model
{
    public class DeviceStatusModel : BaseSocketUpdatedModel
    {
        private bool _IsTcpEnabled = false;
        private bool _IsAllJoynEnabled = false;
        private bool _IsKinectEnabled = false;
        private bool _IsMotorEnabled = false;
        private bool _IsLedEnabled = false;

        public bool IsTcpEnabled {
            get { return _IsTcpEnabled; }
            set
            {
                if (!object.Equals(_IsTcpEnabled, value))
                {
                    _IsTcpEnabled = value;
                    OnPropertyChanged("IsTcpEnabled");
                }
            }
        }

        public bool IsAllJoynEnabled
        {
            get { return _IsAllJoynEnabled; }
            set
            {
                if (!object.Equals(_IsAllJoynEnabled, value))
                {
                    _IsAllJoynEnabled = value;
                    OnPropertyChanged("IsAllJoynEnabled");
                }
            }
        }

        public bool IsKinectEnabled
        {
            get { return _IsKinectEnabled; }
            set
            {
                if (!object.Equals(_IsKinectEnabled, value))
                {
                    _IsKinectEnabled = value;
                    OnPropertyChanged("IsKinectEnabled");
                }
            }
        }

        public bool IsMotorEnabled
        {
            get { return _IsMotorEnabled; }
            set
            {
                if (!object.Equals(_IsMotorEnabled, value))
                {
                    _IsMotorEnabled = value;
                    OnPropertyChanged("IsMotorEnabled");
                }
            }
        }

        public bool IsLedEnabled
        {
            get { return _IsLedEnabled; }
            set
            {
                if (!object.Equals(_IsLedEnabled, value))
                {
                    _IsLedEnabled  = value;
                    OnPropertyChanged("IsLedEnabled");
                }
            }
        }

        public override void HandleSocketUpdate(Request request)
        {
            Boolean valueToSet = false;
            if (request.command == "ServiceIsUp")
                valueToSet = true;
            else if (request.command == "ServiceIsDown")
                valueToSet = false;

            string propertyKey = request.parameters["serviceName"] as string;

            PropertyInfo propertyInfo = this.GetType().GetProperty(propertyKey as string);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(this, Convert.ChangeType(valueToSet, propertyInfo.PropertyType), null);
            }
        }

    }
}
