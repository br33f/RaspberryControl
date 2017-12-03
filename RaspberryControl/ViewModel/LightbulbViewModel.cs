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

        public bool IsWhite
        {
            get { return !IsColor; }
        }

        public bool IsColor
        {
            get { return Model.IsColor; }
            set { Model.IsColor = value; }
        }

        public UInt32 Brightness
        {
            get { return Model.Brightness; }
            set { Model.Brightness = value; }
        }

        public UInt32 ColorTemperature
        {
            get { return Model.ColorTemperature; }
            set { Model.ColorTemperature = value; }
        }

        public UInt32 Hue
        {
            get { return Model.Hue; }
            set { Model.Hue = value; }
        }

        protected void OnModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
            if (e.PropertyName.Equals("IsColor"))
            {
                OnPropertyChanged("IsWhite");
            }
        }
    }
}
