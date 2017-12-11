using RaspberryControl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace RaspberryControl.ViewModel
{
    public class TemperatureSensorViewModel : BaseViewModel
    {
        public TemperatureSensorModel Model { get; set; }

        public TemperatureSensorViewModel()
        {
            this.Model = new TemperatureSensorModel();
            this.Model.PropertyChanged += OnModelPropertyChanged;
        }
        
        public double Temperature
        {
            get { return Model.Temperature; }
            set
            {
                Model.Temperature = value;
            }
        }

        public double Humidity
        {
            get { return Model.Humidity; }
            set
            {
                Model.Humidity = value;
            }
        }

        protected void OnModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }
    }
}
