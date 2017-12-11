using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspberryControl.Service
{
    public class Motor
    {
        private Service.Raspberry _RaspberryService;

        public Motor(Service.Raspberry raspberryService)
        {
            this._RaspberryService = raspberryService;
        }

        public void SpinLeft(string motorName)
        {
            Request request = new Request();
            request.command = "SpinClockwise";
            request.parameters = new Hashtable();
            request.parameters.Add("motorName", motorName);
            _RaspberryService.SendRequest(request);
        }

        public void SpinRight(string motorName)
        {
            Request request = new Request();
            request.command = "SpinCounterClockwise";
            request.parameters = new Hashtable();
            request.parameters.Add("motorName", motorName);
            _RaspberryService.SendRequest(request);
        }

        public void Stop(string motorName)
        {
            Request request = new Request();
            request.command = "Stop";
            request.parameters = new Hashtable();
            request.parameters.Add("motorName", motorName);
            _RaspberryService.SendRequest(request);
        }
    }
}
