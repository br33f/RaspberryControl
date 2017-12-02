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
        private UInt32 _Brightness;

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
