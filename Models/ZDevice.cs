using System;
using System.Collections.Generic;

namespace mz.betainteractive.sigeas.zdbx.models {

    [Serializable]
    public partial class ZDevice {

        public int Id { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public int ConnectionType { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public int BaudRate { get; set; }
        public int ComPort { get; set; }
        public int MaxUsers { get; set; }
        public virtual List<ZDeviceUser> DeviceUsers { get; set; }        
        
        public ZDevice() {            
            this.DeviceUsers = new List<ZDeviceUser>();            
        }
        
    }
}
