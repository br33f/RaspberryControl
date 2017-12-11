using RaspberryControl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace RaspberryControl.ViewModel
{
    public class KinectViewModel : BaseViewModel
    {
        private string _LastCommand = "Brak odebranych rozkazów";
       public string LastCommand
        {
            get { return _LastCommand; }
            set
            {
                if (value != _LastCommand)
                {
                    _LastCommand = value;
                    OnPropertyChanged("LastCommand");
                }
            }
        }
    }
}
