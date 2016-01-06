using System;
using System.Collections.Generic;

namespace mz.betainteractive.sigeas.zdbx.models {

    [Serializable]
    public partial class ZDeviceGroupTimezone {

        public int Id { get; set; }
        public int GroupIndex { get; set; }
        public string Name { get; set; }
        public int ValidHolyday { get; set; }
        public int VerifyStyle { get; set; }        

        public virtual ZDeviceTimezone DeviceTimezone1 { get; set; }
        public virtual ZDeviceTimezone DeviceTimezone2 { get; set; }
        public virtual ZDeviceTimezone DeviceTimezone3 { get; set; }
        public virtual List<ZUser> Users { get; set; }
        
        public ZDeviceGroupTimezone() {
            this.Users = new List<ZUser>();
        }

        
    }
}
