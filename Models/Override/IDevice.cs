using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mz.betainteractive.sigeas.DeviceSystem;

namespace mz.betainteractive.sigeas.Models.Entities {

    public partial class IDevice {

        public bool Connected;
        internal zkemkeeper.CZKEMClass BiometricSDK;
        public event DeviceConnectionEventHandler OnConnectionStateChanged;

        public void Disconnect() {
            if (BiometricSDK != null) {
                BiometricSDK.Disconnect();
            }
        }
                
        public void InitSDK() {
            if (this.BiometricSDK == null) {
                this.BiometricSDK = new zkemkeeper.CZKEMClass();
                this.BiometricSDK.OnConnected += new zkemkeeper._IZKEMEvents_OnConnectedEventHandler(BiometricSDK_OnConnected);
                this.BiometricSDK.OnDisConnected += new zkemkeeper._IZKEMEvents_OnDisConnectedEventHandler(BiometricSDK_OnDisConnected);
            }            
        }

        private void BiometricSDK_OnDisConnected() {
            this.Connected = false;
            fireConnectionStateChanged();
        }                

        private void BiometricSDK_OnConnected() {
            this.Connected = true;
            fireConnectionStateChanged();
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
