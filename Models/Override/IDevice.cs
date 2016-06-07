using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mz.betainteractive.sigeas.DeviceSystem;

namespace mz.betainteractive.sigeas.Models.Entities {

    public partial class IDevice {

        public bool Connected { get { return connected; } }
        private bool connected;
        private int fingerAlgorhytm;
        private bool tftMachine;
        private zkemkeeper.CZKEMClass biometricSDK;
        internal zkemkeeper.CZKEMClass BiometricSDK { get { return biometricSDK; } }
        public event DeviceConnectionEventHandler OnConnectionStateChanged;

        public void Disconnect() {
            if (BiometricSDK != null) {
                BiometricSDK.Disconnect();
            }
        }
                
        public void InitSDK() {
            if (this.BiometricSDK == null) {
                this.biometricSDK = new zkemkeeper.CZKEMClass();
                this.BiometricSDK.OnConnected += new zkemkeeper._IZKEMEvents_OnConnectedEventHandler(BiometricSDK_OnConnected);
                this.BiometricSDK.OnDisConnected += new zkemkeeper._IZKEMEvents_OnDisConnectedEventHandler(BiometricSDK_OnDisConnected);
            }            
        }

        public void TerminateSDK() {
            this.biometricSDK = null;
        }

        public bool IsTFTMachine() {            
            return tftMachine;
        }

        public bool IsZKFinger10() {
            return fingerAlgorhytm == 10;
        }

        private void BiometricSDK_OnDisConnected() {
            this.connected = false;
            this.fingerAlgorhytm = 0;
            this.tftMachine = false;
            fireConnectionStateChanged();
        }                

        private void BiometricSDK_OnConnected() {
            this.connected = true;
            GetStatus();
            fireConnectionStateChanged();
        }

        private void GetStatus() {
            //get ZK Finger Algorhytm
            string versionFp = "";
            if (this.BiometricSDK.GetSysOption(1, "~ZKFPVersion", out versionFp)) {
                if (versionFp != null) {
                    this.fingerAlgorhytm = Int32.Parse(versionFp);
                }
            }

            //is tft machine                
            this.tftMachine = this.BiometricSDK.IsTFTMachine(1);           
        }

        private void fireConnectionStateChanged() {
            if (OnConnectionStateChanged != null) {
                OnConnectionStateChanged(this, new DeviceConnectionEventArgs(this));
            }
        }

        public override string ToString() {
            return this.Name;
        }

        public override bool Equals(object obj) {
            if (obj is IDevice) {
                IDevice d = (IDevice)obj;
                return this.SerialNumber == d.SerialNumber;
            }

            return false;
        }

        public override int GetHashCode() {
            return 0;
        }
                
    }

    public class DeviceConnectionEventArgs : EventArgs {
        private IDevice device;

        public DeviceConnectionEventArgs(IDevice device) {
            this.device = device;
        }

        public IDevice Device {
            get {
                return device;
            }
        }
    }

    public delegate void DeviceConnectionEventHandler(object sender, DeviceConnectionEventArgs e);
}
