using RaspberryControl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace RaspberryControl.ViewModel
{
    public class LightbulbViewModel : BaseViewModel
    {
        public LightbulbModel Model { get; set; }

        public LightbulbViewModel()
        {
            this.Model = new LightbulbModel();
            this.Model.PropertyChanged += OnModelPropertyChanged;
        }

        public bool IsOnOff
        {
            get { return Model.IsOnOff; }
            set
            {
                Model.IsOnOff = value;
            }
        }

        public float Brightness
        {
            get { return (float)(Model.Brightness * 100) / UInt32.MaxValue; }
            set
            {
                UInt32 valueToSet = (UInt32)((value / 100) * UInt32.MaxValue);
                Model.Brightness = valueToSet;
            }
        }

        protected void OnModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }
    }
}
