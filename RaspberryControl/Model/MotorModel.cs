using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RaspberryControl.Model
{
    public class MotorModel : BaseSocketUpdatedModel
    {
        private string _MotorName;

        private bool _IsMovingUp;
        private bool _IsMovingDown;

        public MotorModel(string motorName)
        {
            this._MotorName = motorName;
        }

        public bool IsMovingUp
        {
            get { return _IsMovingUp; }
            set
            {
                if (!object.Equals(_IsMovingUp, value))
                {
                    if (value)
                        Service.Raspberry.Instance.MotorService.SpinLeft(this._MotorName);
                    else
                        Service.Raspberry.Instance.MotorService.Stop(this._MotorName);

                    _IsMovingUp = value;
                    OnPropertyChanged("IsMovingUp");
                }
            }
        }

        public bool IsMovingDown
        {
            get { return _IsMovingDown; }
            set
            {
                if (!object.Equals(_IsMovingDown, value))
                {
                    if (value)
                        Service.Raspberry.Instance.MotorService.SpinRight(this._MotorName);
                    else
                        Service.Raspberry.Instance.MotorService.Stop(this._MotorName);


                    _IsMovingDown = value;
                    OnPropertyChanged("IsMovingDown");
                }
            }
        }

        public override void HandleSocketUpdate(Request request)
        {
            throw new NotImplementedException();
        }
    }
}
