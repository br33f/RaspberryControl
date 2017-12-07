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
    public class Raspberry
    {
        public const string INPUT_SOCK_PORT = "8725";
        public const string OUTPUT_SOCK_PORT = "8724";

        private static Raspberry _Instance;

        // Child services
        public Service.Lightbulb LightbulbService { get; set; }
        public Service.Motor MotorService { get; set; }

        // Input Socket
        private StreamSocket InputSocket;
        private StreamReader InputStreamReader;

        // Output Socket
        private StreamSocket OutputSocket;
        private StreamWriter OutputStreamWriter;

        // ViewModels
        private DeviceStatusViewModel DeviceStatusVM { get; set; }
        private LightbulbViewModel LightbulbVM { get; set; }

        public static void Create(DeviceStatusViewModel deviceStatusVM, LightbulbViewModel lightbulbVM)
        {
            _Instance = new Raspberry(deviceStatusVM, lightbulbVM);
        }

        public static Raspberry Instance
        {
            get { return _Instance; }
        }
        
        private Raspberry(DeviceStatusViewModel deviceStatusVM, LightbulbViewModel lightbulbVM)
        {
            this.DeviceStatusVM = deviceStatusVM;
            this.LightbulbVM = lightbulbVM;

            this.InitializeSockets();
            this.InitializeChildServices();
        }

        private void InitializeSockets()
        {
            this.InitializeOutputSocket();
            this.InitializeInputSocket();
        }

        private void InitializeChildServices()
        {
            this.LightbulbService = new Service.Lightbulb(this);
            this.MotorService = new Service.Motor(this);
        }

        private async void InitializeOutputSocket()
        {
            this.OutputSocket = new StreamSocket();
            await this.OutputSocket.ConnectAsync(new HostName("192.168.0.22"), OUTPUT_SOCK_PORT);

            this.OutputStreamWriter = new StreamWriter(this.OutputSocket.OutputStream.AsStreamForWrite());
        }

        private async void InitializeInputSocket()
        {
            this.InputSocket = new StreamSocket();
            await this.InputSocket.ConnectAsync(new HostName("192.168.0.22"), INPUT_SOCK_PORT);

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

        public void SendRequest(Request request)
        {
            string serializedRequest = JsonConvert.SerializeObject(request);

            this.OutputStreamWriter.WriteLine(Utils.Base64Encode(serializedRequest));
            this.OutputStreamWriter.Flush();
        }

        private void ProcessRequest(Request request)
        {
            BaseSocketUpdatedModel reqModel = null;

            switch (request.command)
            {
                case "ServiceIsUp":
                    reqModel = DeviceStatusVM.Model;
                    break;
                case "ServiceIsDown":
                    reqModel = DeviceStatusVM.Model;
                    break;
                case "LightbulbUpdate":
                    reqModel = LightbulbVM.Model;
                    break;
                default:
                    Utils.LogLine("InputStream ProcessRequest: Nie rozpoznano komendy:" + request.command);
                    break;
            }

            reqModel.HandleSocketUpdate(request);
        }
    }
}
