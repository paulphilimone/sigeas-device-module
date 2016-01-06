using System;
using System.Collections.Generic;
using mz.betainteractive.sigeas.zdbx.models;

namespace mz.betainteractive.sigeas.Models.Entities {

    public partial class IDevice {

        public int Id { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public int ConnectionType { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public int BaudRate { get; set; }
        public int ComPort { get; set; }
        public int MaxUsers { get; set; }

        public IDevice() {
        }

        public IDevice(ZDevice zdevice) {
            this.Id = zdevice.Id;
            this.Name = zdevice.Name;
            this.SerialNumber = zdevice.SerialNumber;
            this.ConnectionType = zdevice.ConnectionType;
            this.IpAddress = zdevice.IpAddress;
            this.Port = zdevice.Port;
            this.BaudRate = zdevice.BaudRate;
            this.ComPort = zdevice.ComPort;
            this.MaxUsers = zdevice.MaxUsers;
        }
    }
}
