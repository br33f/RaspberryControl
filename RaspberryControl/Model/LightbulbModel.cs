using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RaspberryControl.Model
{
    public class LightbulbModel : BaseSocketUpdatedModel
    {
        private bool _IsOnOff;
        private bool _IsColor;
        private UInt32 _Brightness;
        private UInt32 _ColorTemperature;
        private UInt32 _Hue;

        public bool IsOnOff
        {
            get { return _IsOnOff; }
            set
            {
                if (!object.Equals(_IsOnOff, value))
                {
                    if (value)
                        Service.Raspberry.Instance.TurnLightOn();
                    else
                        Service.Raspberry.Instance.TurnLightOff();
                    
                    _IsOnOff = value;
                    OnPropertyChanged("IsOnOff");
                }
            }
        }

        public bool IsColor
        {
            get { return _IsColor; }
            set
            {
                if (!object.Equals(_IsColor, value))
                {
                    Service.Raspberry.Instance.SetIsColor(value);

                    _IsColor = value;
                    OnPropertyChanged("IsColor");
                }
            }
        }

        public UInt32 Brightness
        {
            get { return _Brightness; }
            set
            {
                if (!object.Equals(_Brightness, value))
                {
                    Service.Raspberry.Instance.SetBrightness(value);
                    _Brightness = value;
                    OnPropertyChanged("Brightness");
                }
            }
        }

        public UInt32 ColorTemperature
        {
            get { return _ColorTemperature; }
            set
            {
                if (!object.Equals(_ColorTemperature, value))
                {
                    Service.Raspberry.Instance.SetColorTemperature(value);
                    _ColorTemperature = value;
                    OnPropertyChanged("ColorTemperature");
                }
            }
        }

        public UInt32 Hue
        {
            get { return _Hue; }
            set
            {
                if (!object.Equals(_Hue, value))
                {
                    Service.Raspberry.Instance.SetHue(value);
                    _Hue = value;
                    OnPropertyChanged("Hue");
                }
            }
        }

        public override void HandleSocketUpdate(Request request)
        {
            foreach(DictionaryEntry param in request.parameters)
            {
                PropertyInfo propertyInfo = this.GetType().GetProperty(param.Key as string);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(this, Convert.ChangeType(param.Value, propertyInfo.PropertyType), null);
                }
            }
        }
    }
}
