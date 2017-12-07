using RaspberryControl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace RaspberryControl.ViewModel
{
    public class MotorViewModel : BaseViewModel
    {
        public MotorModel Model { get; set; }

        public void initializeMotorModel(string motorName)
        {
            this.Model = new MotorModel(motorName);
            this.Model.PropertyChanged += OnModelPropertyChanged;
        }

        public bool IsActive
        {
            get { return Model.IsMovingUp || Model.IsMovingDown; }
        }

        public bool IsMovingUp
        {
            get { return Model.IsMovingUp; }
            set
            {
                Model.IsMovingUp = value;
            }
        }

        public bool IsMovingDown
        {
            get { return Model.IsMovingDown; }
            set
            {
                Model.IsMovingDown = value;
            }
        }

        public void btnUp_Click(object sender, RoutedEventArgs e)
        {
            if (IsMovingDown)
                IsMovingDown = false;
            else
                IsMovingUp = !IsMovingUp;
        }

        public void btnDown_Click(object sender, RoutedEventArgs e)
        {
            if (IsMovingUp)
                IsMovingUp = false;
            else
                IsMovingDown = !IsMovingDown;
        }

        protected void OnModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
            OnPropertyChanged("IsActive");
        }
    }
}
