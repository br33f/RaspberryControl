using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspberryControl.Service
{
    public class Lightbulb
    {
        private Service.Raspberry _RaspberryService;

        public Lightbulb(Service.Raspberry raspberryService)
        {
            this._RaspberryService = raspberryService;
        }

        public void TurnLightOn()
        {
            Request request = new Request();
            request.command = "TurnLightOn";
            request.parameters = new Hashtable();
            _RaspberryService.SendRequest(request);
        }

        public void TurnLightOff()
        {
            Request request = new Request();
            request.command = "TurnLightOff";
            request.parameters = new Hashtable();
            _RaspberryService.SendRequest(request);
        }

        public void SetBrightness(UInt32 brightnessValue)
        {
            Request request = new Request();
            request.command = "SetBrightness";
            request.parameters = new Hashtable();
            request.parameters.Add("brightness", brightnessValue);
            _RaspberryService.SendRequest(request);
        }

        public void SetColorTemperature(UInt32 colorTemperature)
        {
            Request request = new Request();
            request.command = "SetColorTemperature";
            request.parameters = new Hashtable();
            request.parameters.Add("colorTemperature", colorTemperature);
            _RaspberryService.SendRequest(request);
        }

        public void SetHue(UInt32 hue)
        {
            Request request = new Request();
            request.command = "SetHue";
            request.parameters = new Hashtable();
            request.parameters.Add("hue", hue);
            _RaspberryService.SendRequest(request);
        }

        public void SetIsColor(bool isColor)
        {
            Request request = new Request();
            request.command = "SetIsColor";
            request.parameters = new Hashtable();
            request.parameters.Add("isColor", isColor);
            _RaspberryService.SendRequest(request);
        }
    }
}
