using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RaspberryControl.Model
{
    public class TemperatureSensorModel : BaseSocketUpdatedModel
    {
        private double _Temperature = 0.0f;
        private double _Humidity = 0.0f;

        public double Temperature
        {
            get { return _Temperature; }
            set
            {
                _Temperature = value;
                OnPropertyChanged("Temperature");
            }
        }

        public double Humidity
        {
            get { return _Humidity; }
            set
            {
                _Humidity = value;
                OnPropertyChanged("Humidity");
            }
        }

        public override void HandleSocketUpdate(Request request)
        {
            foreach (DictionaryEntry param in request.parameters)
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
