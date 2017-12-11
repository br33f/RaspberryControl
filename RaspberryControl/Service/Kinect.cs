using Newtonsoft.Json;
using RaspberryControl.Model;
using RaspberryControl.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;

namespace RaspberryControl.Service
{
    public class Kinect
    {
        public const string INPUT_SOCK_PORT = "8726";

        private static Kinect _Instance;

        // Input Socket
        private StreamSocket InputSocket;
        private StreamReader InputStreamReader;


        // ViewModels
        private DeviceStatusViewModel DeviceStatusVM { get; set; }
        private LightbulbViewModel LightbulbVM { get; set; }
        private MotorViewModel MotorAVM { get; set; }
        private MotorViewModel MotorBVM { get; set; }
        private KinectViewModel KinectVM { get; set; }

        public static void Create(DeviceStatusViewModel deviceStatusVM, LightbulbViewModel lightbulbVM, MotorViewModel motorAVM, MotorViewModel motorBVM, KinectViewModel kinectVM)
        {
            _Instance = new Kinect(deviceStatusVM, lightbulbVM, motorAVM, motorBVM, kinectVM);
        }

        public static Kinect Instance
        {
            get { return _Instance; }
        }
        
        private Kinect(DeviceStatusViewModel deviceStatusVM, LightbulbViewModel lightbulbVM, MotorViewModel motorAVM, MotorViewModel motorBVM, KinectViewModel kinectVM)
        {
            this.DeviceStatusVM = deviceStatusVM;
            this.LightbulbVM = lightbulbVM;
            this.MotorAVM = motorAVM;
            this.MotorBVM = motorBVM;
            this.KinectVM = kinectVM;

            this.InitializeSockets();
        }

        private void InitializeSockets()
        {
            this.InitializeInputSocket();
        }
     

        private async void InitializeInputSocket()
        {
            this.InputSocket = new StreamSocket();
            await this.InputSocket.ConnectAsync(new HostName("192.168.0.20"), INPUT_SOCK_PORT);

            this.InputStreamReader = new StreamReader(this.InputSocket.InputStream.AsStreamForRead());

            try
            {
                while (true)
                {
                    string rawRequest = await this.InputStreamReader.ReadLineAsync();
                    string decodedRequest = Utils.Base64Decode(rawRequest);
                    Utils.LogLine("Odebrano: " + decodedRequest);

                    Request request = JsonConvert.DeserializeObject<Request>(decodedRequest);
                    ProcessRequest(request);
                }
            } catch (Exception e)
            {
                Utils.LogLine(e.Message);
            }
        }

        private void ProcessRequest(Request request)
        {
            BaseSocketUpdatedModel reqModel = null;

            switch (request.command)
            {
                case "Ping":
                    break;
                case "ServiceIsUp":
                    reqModel = DeviceStatusVM.Model;
                    break;
                case "ServiceIsDown":
                    reqModel = DeviceStatusVM.Model;
                    break;
                case "PullRight":
                    string direction = request.parameters["direction"] as string;
                    HandleMove(MotorAVM.Model, direction);
                    string lastCommand = "Prawa ręka - pociągnięcię w ";
                    lastCommand += (direction.Equals("up")) ? "górę" : "dół";
                    KinectVM.LastCommand = lastCommand;
                    break;
                case "Cross":
                    LightbulbVM.Model.IsOnOff = !LightbulbVM.Model.IsOnOff;
                    KinectVM.LastCommand = "Skrzyżowanie rąk";
                    break;
                default:
                    Utils.LogLine("InputStream ProcessRequest: Nie rozpoznano komendy:" + request.command);
                    break;
            }

            if (reqModel != null)
            {
                reqModel.HandleSocketUpdate(request);
            }
        }

        private void HandleMove(MotorModel motor, string direction)
        {
            if (direction == "up")
            {
                motor.IsMovingUp = !motor.IsMovingUp;
            }
            else
            {
                motor.IsMovingDown = !motor.IsMovingDown;
            }
        }
    }
}
