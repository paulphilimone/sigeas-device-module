using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mz.betainteractive.sigeas.BackgroundFeatures;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading;
using mz.betainteractive.sigeas.Utilities;
using mz.betainteractive.sigeas.Models.Entities;

namespace mz.betainteractive.sigeas.DeviceSystem {

    public class DeviceConnector {

        private zkemkeeper.CZKEMClass BioAccess;
        private IDevice device;
        private int ConnectionType; //0-IP, 1-RS, 2-USB
        private string IpAddress;
        private int IpPort;
        private int ComPort;
        private int BaudRate;
        private bool connected;
        private int connecting;
        public bool eventsRegistered;       
        private BackgroundWorker backgroundWorkerConnect;
        private LoadingWindow loadingWindow;

        public DeviceConnector(zkemkeeper.CZKEMClass access) {
            this.BioAccess = access;
            InitComponents();
        }                

        public DeviceConnector(IDevice device) {
            this.device = device;                        
            device.InitSDK();            

            this.BioAccess = device.BiometricSDK;

            InitComponents();
            InitDevice();
        }               

        public static DeviceConnector GetDeviceConnector(){
            zkemkeeper.CZKEMClass zkem = new zkemkeeper.CZKEMClass();
            return new DeviceConnector(zkem);
        }

        public static DeviceConnector GetDeviceConnector(string ipAddress, int port) {
            zkemkeeper.CZKEMClass zkem = new zkemkeeper.CZKEMClass();
            DeviceConnector dc = new DeviceConnector(zkem);
            dc.SetTCPConnectionMode(ipAddress, port);
            return dc;
        }
        
        public static DeviceConnector GetDeviceConnector(int comPort, int baudRate) {
            zkemkeeper.CZKEMClass zkem = new zkemkeeper.CZKEMClass();
            DeviceConnector dc = new DeviceConnector(zkem);
            dc.SetCOMConnectionMode(comPort, baudRate);
            return dc;
        }

        public static DeviceConnector GetDeviceConnector(IDevice device) {            
            return new DeviceConnector(device);
        }

        private void InitComponents() {
            backgroundWorkerConnect = new BackgroundWorker();
            backgroundWorkerConnect.DoWork += new DoWorkEventHandler(backgroundWorkerConnect_DoWork);
            backgroundWorkerConnect.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorkerConnect_RunWorkerCompleted);

            this.loadingWindow = new LoadingWindow();             
        }

        private void InitDevice() {
            this.ConnectionType = device.ConnectionType;
            this.IpAddress = device.IpAddress;
            this.IpPort = device.Port;
            this.ComPort = device.ComPort;
            this.BaudRate = device.BaudRate;
        }

        public void StartConnection() {
            try {
                // Init ProgressBar
                loadingWindow.SetTitle("Conectando....");
                loadingWindow.LabelTextProgress.Text = "Conectando ao biométrico...";

                // Run Application with ProgressBar
                this.connecting = 1;
                backgroundWorkerConnect.RunWorkerAsync();
                //backgroundWorker.RunWorkerAsync("test");

                loadingWindow.ShowDialog();

            } catch {
            }

        }

        private void ConnectTest() {
            try {
                
                this.connected = ConnectDevice();

                if (this.connected == false) {
                    MessageBox.Show("Não foi possivel conectar-se ao dispositivo", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    if (this.device != null) {
                        //this.device.Connected = false;
                        this.device.TerminateSDK();
                    }

                    return;
                }

                //is connected

                if (device != null) {
                    string sn = "";
                    this.device.BiometricSDK.GetSerialNumber(1, out sn);

                    if (sn.Length == 0) {
                        MessageBox.Show("Não foi possivel obter o (número de série) do dispositivo, a conexão será desfeita", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.device.BiometricSDK.Disconnect();
                        //this.device.Connected = false;
                        this.device.TerminateSDK();
                        return;
                    }

                    if (!System.Diagnostics.Debugger.IsAttached)
                    if (sn != device.SerialNumber) {
                        MessageBox.Show("O (número de série) do biometrico registrado no sistema é diferente a do biométrico que foi conectado, Por causa desta incompatibilidade a conexão será desfeita", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.device.BiometricSDK.Disconnect();
                        //this.device.Connected = false;
                        this.device.TerminateSDK();
                        return;
                    }
                }                                
                                
            } catch (System.FormatException ex) {
                MessageBox.Show("Ocorreu um erro ao tentar conectar ao biometrico", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogErrors.AddErrorLog(ex, "Ocorreu um erro ao tentar conectar ao biometrico");
                
                if (device != null) {
                    //this.device.Connected = false;
                    this.device.TerminateSDK();
                }

                return;
            }

        }
                
        void backgroundWorkerConnect_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            this.loadingWindow.Close();
        }

        void backgroundWorkerConnect_DoWork(object sender, DoWorkEventArgs e) {
            ConnectTest();
        }                

        public void SetTCPConnectionMode(string ipAddress, int port) {
            this.ConnectionType = 0;
            this.IpAddress = ipAddress;
            this.IpPort = port;
        }

        public void SetCOMConnectionMode(int comPort, int baudRate) {
            this.ConnectionType = 1;
            this.ComPort = comPort;
            this.BaudRate = baudRate;
        }

        public void SetUSBConnectionMode() {
            this.ConnectionType = 2;
        }

        private bool ConnectDevice() {
            try {

                connecting = 1;
                Console.WriteLine("Connecting");
                //BioAccess.Disconnect();

                if (this.ConnectionType == 0) {
                    this.connected = BioAccess.Connect_Net(this.IpAddress, this.IpPort);                    
                }
                
                if (this.ConnectionType == 1) {
                    this.connected = BioAccess.Connect_Com(this.ComPort, 1, this.BaudRate);                        
                }

                if (this.ConnectionType == 2) {
                    this.connected = BioAccess.Connect_USB(1);                    
                }

                if (this.connected) {
                    bool regev = this.BioAccess.RegEvent(1, 65535);
                    this.eventsRegistered = regev;
                    //Console.WriteLine("All Events Registred " + regev);                    
                }

                connecting = 2;

                return this.connected;

            } catch (Exception ex) {
                MessageBox.Show("Ocorreu erro ao tentar conectar-se ao biométrico");
                return false;
            }
        }

        public bool IsConnected() {
            return this.connected;
        }

        public bool IsEventsRegistered() {
            return this.eventsRegistered;
        }

        private zkemkeeper.CZKEMClass GetBiometricSDK() {
            return this.BioAccess;
        }

        public void GetSerialNumber(out string serialNumber){

            serialNumber = "";

            if (this.IsConnected()){
                this.BioAccess.GetSerialNumber(1, out serialNumber);
            }
            
        }

        public void GetDeviceStatus(int status, ref int value){
                        
            if (this.IsConnected()) {
                this.BioAccess.GetDeviceStatus(1, status, ref value);
            }
        }

        public void GetDeviceStatusStr(int status, out string status_info){
            status_info = "";
            if (this.IsConnected()) {
                this.BioAccess.GetDeviceStrInfo(1, status, out status_info);
            }
        }

        public void GetProductCode(out string ProductName){
            ProductName = "";
            if (this.IsConnected()) {
                this.BioAccess.GetProductCode(1, out ProductName);
            }
        }
                
        public void Disconnect() {
            if (this.BioAccess != null) {
                
                this.BioAccess.Disconnect();
            }
        }
    }
}
